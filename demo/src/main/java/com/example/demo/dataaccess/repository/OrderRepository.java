package com.example.demo.dataaccess.repository;

import com.example.demo.dataaccess.entity.Order;
import org.springframework.data.jpa.repository.JpaRepository;

public interface OrderRepository extends JpaRepository<Order, Long> {
}
