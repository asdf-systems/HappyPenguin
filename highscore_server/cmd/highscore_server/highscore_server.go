package main

import (
	"net/http"
	"jsonrpc"
	"log"
	"io"
	"os"
	"bytes"
)

var (
	rpc *jsonrpc.JsonRPC
	db  = NewHighscoreDB(".")
)

func main() {
	setupLogger()
	log.Printf("Starting highscore server\n")

	rpc = jsonrpc.New(db)

	http.HandleFunc("/", handler)
	e := http.ListenAndServe(":8080", nil)
	if e != nil {
		log.Fatalf("Could not start server: %s\n", e)
	}
}

func handler(w http.ResponseWriter, r *http.Request) {
	log.Printf("Handling %s\n", r.RemoteAddr)
	buf := new(bytes.Buffer)
	io.Copy(buf, r.Body)

	data, e := rpc.Execute(buf.String())
	if e != nil {
		log.Printf("Could not handle %s request: %s\n", r.RemoteAddr, e)
	}
	w.Write([]byte(data))
}

func setupLogger() {
	f, e := os.OpenFile("log.txt", os.O_CREATE|os.O_APPEND|os.O_WRONLY, 0644)
	if e != nil {
		panic(e)
	}
	log.SetOutput(f)
}
