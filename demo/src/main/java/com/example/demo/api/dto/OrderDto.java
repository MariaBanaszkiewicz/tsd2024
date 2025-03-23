package com.example.demo.api.dto;

import com.example.demo.common.Status;
import com.example.demo.dataaccess.entity.Product;

import java.util.List;

public record OrderDto(List<Long> products, String customerName, String customerEmail) {
}
