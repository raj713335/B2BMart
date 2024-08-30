using AutoMapper;
using B2BMart.API.Errors;
using B2BMart.API.Services;
using B2BMart.DataLayer;
using B2BMart.DataLayer.Models;
using B2BMart.DataLayer.UOW;
using B2BMart.Models;
using B2BMart.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace B2BMart.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public AccountController(IUnitOfWork uow, IMapper mapper, ITokenService tokenService)
        {
            _uow = uow;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        

        [HttpGet("emailexists/{email}")]
        public ActionResult<bool> CheckEmailExistsAsync(string email)
        {
            var data = _uow.UserRepository.FindAsQueryable(x => x.EmailId == email).FirstOrDefault();
            if ((data) == null)
            {
                return Ok(false);
            }
            return Ok(data);
        }

        [Authorize]
        [HttpGet("allusers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<User>>> GetAllUserDetails()
        {
            var users = await _uow.UserRepository.ListAllAsync();

            if (users == null) return NotFound(new ApiResponse(404));

            return Ok(users);
        }

        [Authorize]
        [HttpGet("getalluseraddress/{id}")]
        public async Task<ActionResult<Address>> GetUserAddress(int id)
        {
            var user_address =  _uow.AddressRepository.FindAsQueryable(x => x.UserId == id).FirstOrDefault();

            return Ok(user_address);
        }

       

        [HttpPost("register")]
        public ActionResult<AppUserDTO> Register([FromBody] AppUser user)
        {
            if (UserNameExists(user.UserName.ToLower()))
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email addess is in use" } });
                //BadRequest("Username is taken");

            if (UserEmailExists(user.EmailId.ToLower()))
                return BadRequest("An account associated with this email already exists. Try forget password.");

            using var hmac = new HMACSHA512();

            var newUser = _mapper.Map<User>(user);

            newUser.UserName = user.UserName.ToLower();
            newUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
            newUser.PasswordSalt = hmac.Key;

            _uow.UserRepository.Add(newUser);
            _uow.SaveChanges(user.UserName);
            try
            {
                _uow.Commit();
            }
            catch (Exception ex)
            {
                _uow.RollBack();
            }

            var dto = _mapper.Map<AppUserDTO>(user);
            dto.UserId = newUser.UserId;
            dto.Token = _tokenService.CreateToken(user);

            return Ok(dto);
        }

        [HttpPut("updateAddress")]
        [Authorize]
        public ActionResult<AppUserDTO> UpdateUserAddress([FromBody] AddressDTO address)
        {
            var newAddress = _mapper.Map<Address>(address);

            var existingUser = _uow.UserRepository.Find(address.UserId);
            if (existingUser != null)
            {
                var existingaddress = _uow.AddressRepository.FindAsQueryable(x => x.UserId == address.UserId).FirstOrDefault();
                if (existingaddress != null)
                {
                    existingaddress.Address1 = address.Address1;
                    existingaddress.State = address.State;
                    existingaddress.City = address.City;
                    existingaddress.Country = address.Country;
                    existingaddress.AddressType = address.AddressType.ToString();
                    _uow.SaveChanges(existingUser.UserName);
                    try
                    {
                        _uow.Commit();
                    }
                    catch (Exception ex)
                    {
                        _uow.RollBack();
                    }
                }
                else
                {
                    _uow.AddressRepository.Add(newAddress);
                    _uow.SaveChanges(existingUser.UserName);
                    try
                    {
                        _uow.Commit();
                    }
                    catch (Exception ex)
                    {
                        _uow.RollBack();
                    }
                }
            }
            else
            {
                _uow.AddressRepository.Add(newAddress);
                _uow.SaveChanges(existingUser.UserName);
                try
                {
                    _uow.Commit();
                }
                catch (Exception ex)
                {
                    _uow.RollBack();
                }
            }

           

            var dto = new AppUserDTO
            {
                UserId = address.UserId,
                Username = existingUser.UserName,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                EmailId = existingUser.EmailId,
            };

            var userObj = _mapper.Map<AppUser>(existingUser);
            dto.Token = _tokenService.CreateToken(userObj);

            return Ok(dto);
        }

        [HttpPost("login")]
        public ActionResult<AppUserDTO> Login([FromBody] LoginDTO loginDTO)
        {
            var user = _uow.UserRepository.FindAsQueryable(x => x.UserName == loginDTO.Username.ToLower()).SingleOrDefault();

            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized(new ApiResponse(401));
                }
            }
            var userObj = _mapper.Map<AppUser>(user);
            var mappedUser = _mapper.Map<AppUserDTO>(userObj);
            mappedUser.Token = _tokenService.CreateToken(userObj);
            mappedUser.UserId = user.UserId;
            return mappedUser;
        }


        #region private methods
        private bool UserEmailExists(string v)
        {
            return _uow.UserRepository.FindAsQueryable(x => x.EmailId == v).Any();
        }

        private bool UserNameExists(string v)
        {
            return _uow.UserRepository.FindAsQueryable(x => x.UserName == v).Any();
        }
        #endregion
    }
}
