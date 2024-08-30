package com.b2bmart.api.payload;

import lombok.Data;

@Data
public class LoginDto {
    private String email;
    private String password;
}
