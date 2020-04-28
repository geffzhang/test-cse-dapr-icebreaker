package main

import (
	"context"
	"log"
	"loyalty-service/controllers/dapr"
	"loyalty-service/controllers/loyalty"
	"net/http"
	"strings"

	cloudevents "github.com/cloudevents/sdk-go/v2"
	"github.com/gorilla/mux"
	"github.com/rs/cors"
)

const (
	serverPort = ":5400"
)

func main() {
	ctx := context.Background()
	p, err := cloudevents.NewHTTP()
	if err != nil {
		log.Fatalf("failed to create protocol: %s", err.Error())
	}

	h, err := cloudevents.NewHTTPReceiveHandler(ctx, p, loyalty.UpdateLoyalty)
	if err != nil {
		log.Fatalf("failed to create handler: %s", err.Error())
	}

	// router implementation
	router := mux.NewRouter()

	// adding cors
	c := cors.AllowAll()
	handler := c.Handler(router)

	// router handlers
	router.Handle("/order", h).Methods("POST")

	// listen and serve
	log.Printf("will listen on %v\n", serverPort)
	if err := http.ListenAndServe(serverPort, trailingSlashHandler(handler)); err != nil {
		log.Fatalf("unable to start http server, %s", err)
	}
}

func trailingSlashHandler(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		if r.URL.Path != "/" {
			r.URL.Path = strings.TrimSuffix(r.URL.Path, "/")
		}
		next.ServeHTTP(w, r)
	})
}
