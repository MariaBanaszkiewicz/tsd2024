package com.example.demo.api.controller;

import com.example.demo.api.dto.OrderDto;
import com.example.demo.common.Status;
import com.example.demo.dataaccess.entity.Order;
import com.example.demo.dataaccess.repository.OrderRepository;
import com.example.demo.dataaccess.repository.ProductRepository;
import jakarta.validation.Valid;
import lombok.AllArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.validation.FieldError;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.Map;
import java.util.Optional;

@RestController
@RequestMapping("order")
@AllArgsConstructor
public class OrderController {

    private final OrderRepository orderRepository;
    private final ProductRepository productRepository;

    @ResponseStatus(HttpStatus.BAD_REQUEST)
    @ExceptionHandler(MethodArgumentNotValidException.class)
    public Map<String, String> handleValidationExceptions(
            MethodArgumentNotValidException ex) {
        Map<String, String> errors = new HashMap<>();
        ex.getBindingResult().getAllErrors().forEach((error) -> {
            String fieldName = ((FieldError) error).getField();
            String errorMessage = error.getDefaultMessage();
            errors.put(fieldName, errorMessage);
        });
        return errors;
    }

    @PostMapping()
    public Order createOrder(@RequestBody @Valid OrderDto orderDto){
        Order order = new Order();
        order.setProducts(orderDto.products().stream().map(productRepository::findById).map(Optional::orElseThrow).toList());
        order.setCustomerName(orderDto.customerName());
        order.setCustomerEmail(orderDto.customerEmail());
        order.setOrderStatus(Status.PLACED);
        return orderRepository.save(order);
    }

    @PutMapping("/{id}")
    public Order updateStatus(@PathVariable Long id, @RequestParam Status status){
        Order order = orderRepository.findById(id).orElseThrow();

        order.setOrderStatus(status);

        return orderRepository.save(order);

    }

}
