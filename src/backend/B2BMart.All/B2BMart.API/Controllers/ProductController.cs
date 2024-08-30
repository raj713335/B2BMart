using B2BMart.DataLayer.Models;
using B2BMart.DataLayer.UOW;
using B2BMart.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using B2BMart.Models.DTOs;
using B2BMart.DataLayer.Repositories.Specifications;
using B2BMart.API.Errors;
using AutoMapper;
using B2BMart.API.Utility;

namespace B2BMart.API.Controllers
{
    public class ProductController : BaseController
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ProductController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet("getAllProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetAllProductsAsync([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

            var totalItems = await _uow.ProductRepository.CountAsync(countSpec);

            var products = await _uow.ProductRepository.ListAsync(spec);
            
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products);

            if (data == null) return NotFound(new ApiResponse(404));

            return Ok(new Pagination<ProductDTO>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        [HttpGet("getProductById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> GetProductByIdAsync(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product =  await _uow.ProductRepository.GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<ProductDTO>(product);
        }

        

        [HttpGet("getAllProductBrands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllProductBrandAsync()
        {
            var productsbrand = await _uow.ProductBrandRepository.ListAllAsync();

            if (productsbrand == null) return NotFound(new ApiResponse(404));

            return Ok(productsbrand);
        }

        [HttpGet("getBrandById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductBrand>> GetBrandByIdAsync(int id)
        {
            var brand = await _uow.ProductBrandRepository.FindAsQueryable( x => x.Id == id).SingleOrDefaultAsync();

            if (brand == null) return NotFound(new ApiResponse(404));

            return Ok(brand);
        }


        [HttpGet("getAllProductTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllProductTypeAsync()
        {
            var productstype = await _uow.ProductTypeRepository.ListAllAsync();

            if (productstype == null) return NotFound(new ApiResponse(404));
            
            return Ok(productstype);
        }


        [HttpGet("getTypeById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductType>> GetTypeByIdAsync(int id)
        {
            var type =  await _uow.ProductTypeRepository.FindAsQueryable(x => x.Id == id).SingleOrDefaultAsync();

            if (type == null) return NotFound(new ApiResponse(404));

            return Ok(type);
        }



        [HttpPost("AddProductBrand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ProductBrand> AddProductBrand([FromBody] ProductBrandDTO brand)
        {
            if (ProductBrandExists(brand.Name.ToLower()))
                return BadRequest("Product Brand is taken");

            var newbrand = new ProductBrand()
            {
                ProductBrandName = brand.Name
            };

            _uow.ProductBrandRepository.Add(newbrand);
            _uow.SaveChanges(brand.UserName);
            try
            {
                _uow.Commit();
            }
            catch (Exception ex)
            {
                _uow.RollBack();
            }

            return Ok(newbrand);
        }

        [HttpPost("AddProductType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ProductType> AddProductType([FromBody] ProductTypeDTO productType)
        {
            if (ProductTypeExists(productType.Name.ToLower()))
                return BadRequest("Product Type is taken");

            var newproductType = new ProductType
            {
                ProductTypeName = productType.Name
            };

            _uow.ProductTypeRepository.Add(newproductType);
            _uow.SaveChanges(productType.UserName);
            try
            {
                _uow.Commit();
            }
            catch (Exception ex)
            {
                _uow.RollBack();
            }

            return Ok(newproductType);
        }

        [HttpPost("AddProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public ActionResult<Product> AddProduct([FromBody] ProductAddDTO product)
        {
            if (ProductCodeExists(product.ProductCode.ToLower()))
                return BadRequest("Product Code Is Already Taken");

            if (!ProductBrandIdExists(product.ProductBrand))
                return BadRequest("Product Brand Id Doesn't Exists");

            if (!ProductTypeIdExists(product.ProductType))
                return BadRequest("Product Type Id Doesn't Exists");

            if (!UserNameExists(product.CreatedBy))
                return BadRequest("Created By User Doesn't Exists");

           

            try
            {
                var newProduct = new Product
                {
                    ProductName = product.ProductName,
                    ProductCode = product.ProductCode,
                    Description = product.Description,
                    PictureUrl = product.PictureUrl,
                    ProductBrandId = product.ProductBrand,
                    ProductTypeId = product.ProductType,
                    Price = product.Price,
                    Sellerid = product.Sellerid,
                    CreatedBy = product.CreatedBy
                };
                _uow.ProductRepository.Add(newProduct);
                _uow.SaveChanges(product.CreatedBy);
                try
                {
                    _uow.Commit();
                }
                catch (Exception ex)
                {
                    _uow.RollBack();
                }
            }
            catch
            {
                return NotFound(new ApiResponse(500));
            }

            var dto = _mapper.Map<ProductAddDTO>(product);

            return Ok(dto);
        }

        #region private methods
        private bool ProductBrandExists(string brand)
        {
            return _uow.ProductBrandRepository.FindAsQueryable(x => x.ProductBrandName == brand).Any();
        }

        private bool ProductBrandIdExists(int brandid)
        {
            return _uow.ProductBrandRepository.FindAsQueryable(x => x.Id == brandid).Any();
        }

        private bool ProductTypeExists(string type)
        {
            return _uow.ProductTypeRepository.FindAsQueryable(x => x.ProductTypeName == type).Any();
        }

        private bool ProductTypeIdExists(int typeid)
        {
            return _uow.ProductTypeRepository.FindAsQueryable(x => x.Id == typeid).Any();
        }

        private bool UserNameExists(string v)
        {
            return _uow.UserRepository.FindAsQueryable(x => x.UserName == v).Any();
        }

        private bool ProductCodeExists(string productCode)
        {
            return _uow.ProductRepository.FindAsQueryable(x => x.ProductCode == productCode).Any();
        }

        #endregion
    }
}
