package com.example.demo.dataaccess.entity;


import jakarta.persistence.*;
import jakarta.validation.constraints.NotBlank;
import lombok.Getter;
import lombok.Setter;

import java.util.List;

@Entity
@Table(name = "product")
@Getter
@Setter
public class Product {

    @Id
    @GeneratedValue
    private Long id;

    @NotBlank(message = "Name is mandatory")
    private String name;


    @NotBlank(message = "Description is mandatory")
    private String description;


//    @ManyToMany(mappedBy = "products")
//    private List<Order> orders;

}
