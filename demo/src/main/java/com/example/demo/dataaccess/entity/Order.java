package com.example.demo.dataaccess.entity;


import com.example.demo.common.Status;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotBlank;
import lombok.Getter;
import lombok.Setter;

import java.util.List;

@Entity
@Table(name = "orders")
@Getter
@Setter
public class Order {

    @Id
    @GeneratedValue
    private Long id;

    @ManyToMany
    private List<Product> products;

    @NotBlank(message = "Name is mandatory")
    private String customerName;

    @NotBlank(message = "Email is mandatory")
    private String customerEmail;

    private Status orderStatus;
}
