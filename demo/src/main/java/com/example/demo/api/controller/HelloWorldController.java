package com.example.demo.api.controller;

import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("hello")
@RequiredArgsConstructor
public class HelloWorldController {

    @Value("${app.demo.secret_name}")
    private String secretName;

    @GetMapping("/{name}")
    public String getHelloWorld(@PathVariable String name){
        if (name.equals(secretName)) {
            return "Nice to see You!";
        }

        return switch (name){
            case null -> "Hello World!";
            default -> "Hello " + name + " !";

        };
    }
}
