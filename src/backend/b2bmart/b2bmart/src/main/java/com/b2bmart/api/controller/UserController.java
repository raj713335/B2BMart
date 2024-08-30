package com.b2bmart.api.controller;

import com.b2bmart.api.payload.LoginDto;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletRequest;

@RestController
@CrossOrigin("*")
@RequestMapping("/api/user")
public class UserController {

    @PostMapping(path = "/login",consumes = MediaType.APPLICATION_JSON_VALUE ,produces = MediaType.APPLICATION_OCTET_STREAM_VALUE)
    public ResponseEntity<String> login(@RequestBody LoginDto loginDto) {
        String email = loginDto.getEmail();
        return new ResponseEntity<>("login successful = "+email, HttpStatus.OK);
    }
}
