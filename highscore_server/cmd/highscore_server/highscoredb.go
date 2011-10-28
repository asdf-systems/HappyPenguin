package main

import (
	"json"
	"log"
	"os"
	"fmt"
	"time"
)

const (
	TOP10_FILE = "top10.txt"
	ALL_FILE   = "all.txt"
	NUM_PLACES = 10
	PASSWORD = `Iof+:*NLx^%~+zu?-,|`
)

type Entry struct {
	Name   string
	Points int
}

type HighscoreDB struct {
	path   string
	diskop chan diskop_command
	top10  []Entry
}

func NewHighscoreDB(path string) *HighscoreDB {
	hsdb := &HighscoreDB{path: path}
	hsdb.readTop10()
	hsdb.diskop = make(chan diskop_command)
	hsdb.top10 = make([]Entry, NUM_PLACES)
	go hsdb.diskOperator()
	return hsdb
}

func (this *HighscoreDB) AddEntry(e *Entry, pw *string) {
	if *pw != PASSWORD {
		return
	}
	cmd := diskop_command{Entry: *e}
	cmd.ok = make(chan bool)
	this.diskop <- cmd
	<-cmd.ok
}

func (this *HighscoreDB) GetHighscore() []Entry {
	return this.top10
}

func (this *HighscoreDB) readTop10() {
	f, e := os.Open(this.path + "/" + TOP10_FILE)
	if e != nil {
		log.Printf("Could not open top10 file: %s\n", e.String())
		log.Printf("Starting over\n")
		return
	}
	defer f.Close()

	decoder := json.NewDecoder(f)
	e = decoder.Decode(&this.top10)
	if e != nil {
		log.Printf("Could not decode top10 file: %s\n", e.String())
		log.Printf("Starting over. Backing up old file\n")
		e := os.Rename(TOP10_FILE, fmt.Sprintf("%s-%s.txt", TOP10_FILE, time.LocalTime().String()))
		if e != nil {
			log.Printf("Could not backup old file: %s\n", e.String())
			log.Printf("Not caring.")
		}
	}
}

func (this *HighscoreDB) saveTop10() {
	f, e := os.OpenFile(this.path+"/"+TOP10_FILE, os.O_CREATE|os.O_TRUNC|os.O_WRONLY, 0644)
	if e != nil {
		log.Printf("Could not save top10 file: %s\n", e.String())
	}
	defer f.Close()

	encoder := json.NewEncoder(f)
	e = encoder.Encode(this.top10)
	if e != nil {
		log.Printf("Could not encode top10 file: %s\n", e.String())
	}
}

func (this *HighscoreDB) saveNewEntry(entry Entry) {
	f, e := os.OpenFile(this.path+"/"+ALL_FILE, os.O_CREATE|os.O_APPEND|os.O_WRONLY, 0644)
	if e != nil {
		log.Printf("Could not open file for new entry: %s\n", e.String())
	}
	defer f.Close()

	encoder := json.NewEncoder(f)
	e = encoder.Encode(entry)
	if e != nil {
		log.Printf("Could not encode new entry: %s\n", e.String())
	}
}
