package models

import (
	"fmt"
	"time"
)

func init() {
	fmt.Println("Initialize - models package")
}

// KeyValue struct
type KeyValue struct {
	Key   string         `json:"key"`
	Value []OrderSummary `json:"value"`
}

// OrderItemSummary struct
type OrderItemSummary struct {
	MenuItemID   int    `json:"menuItemId"`
	MenuItemName string `json:"menuItemName"`
	Quantity     int    `json:"quantity"`
}

// OrderSummary struct
type OrderSummary struct {
	OrderID      string             `json:"orderId"`
	OrderDate    time.Time          `json:"orderDate"`
	StoreID      string             `json:"storeId"`
	CustomerName string             `json:"customerName"`
	LoyaltyID    string             `json:"loyaltyId"`
	OrderItems   []OrderItemSummary `json:"orderItems"`
	OrderTotal   float64            `json:"orderTotal"`
}
