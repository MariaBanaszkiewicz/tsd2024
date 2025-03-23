package com.example.demo.dataaccess.repository;

import com.example.demo.dataaccess.entity.Product;
import org.springframework.data.jpa.repository.JpaRepository;

public interface ProductRepository extends JpaRepository<Product, Long> {
}
