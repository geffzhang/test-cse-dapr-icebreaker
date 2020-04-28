package makeline

import (
	"bytes"
	"context"
	"encoding/json"
	"fmt"
	"io/ioutil"
	"make-line-service/models"
	"net/http"

	cloudevents "github.com/cloudevents/sdk-go/v2"
	"github.com/gorilla/mux"
)

func init() {
	fmt.Println("Initialize - makeline package")
}

// MakeOrder function
func MakeOrder(ctx context.Context, event cloudevents.Event) {

	// deserialize incoming order summary
	var orderSummary models.OrderSummary
	json.Unmarshal(event.Data(), &orderSummary)

	fmt.Printf("\nReceived Order: %v", orderSummary)

	// TODO: Challenge 4 - Load current list of orders to be made from state store
	   //                   - Add incoming order to the list
	   //                   - Save modified list to state store

	   // TODO: Challenge 6 - Call the SignalR output binding with the incoming order summary
}

// TODO: Challenge 4 - Implement a method to get all orders
//                   - Implement a method to delete an order
