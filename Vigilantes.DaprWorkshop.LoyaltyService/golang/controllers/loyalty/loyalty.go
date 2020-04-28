package loyalty

import (
	"bytes"
	"context"
	"encoding/json"
	"fmt"
	"io/ioutil"
	"loyalty-service/models"
	"math"
	"net/http"

	cloudevents "github.com/cloudevents/sdk-go/v2"
)

func init() {
	fmt.Println("Initialize - loyalty package")
}

// UpdateLoyalty function
func UpdateLoyalty(ctx context.Context, event cloudevents.Event) {

	// deserialize incoming order summary
	var orderSummary models.OrderSummary
	json.Unmarshal(event.Data(), &orderSummary)

	fmt.Printf("\nreceived order: %v", orderSummary)

	// TODO: Challenge 3 - Retrieve and update customer loyalty points
}

