package com.b2bmart.api.controller;

import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@CrossOrigin("*")
public class HomeController {

    @RequestMapping(path = "/",  produces = MediaType.APPLICATION_OCTET_STREAM_VALUE)
    public String home() {
        return "REST API Home";
    }
}
