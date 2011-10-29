package main

import (
	"sort"
)

type diskop_command struct {
	Entry
	ok chan bool
}

func (this *HighscoreDB) diskOperator() {
	for entry := range this.diskop {
		tmp := append(this.top10, entry.Entry)
		sort.Sort(Entries(tmp))
		this.top10 = tmp[0:NUM_PLACES]
		this.saveTop10()
		this.saveNewEntry(entry.Entry)
		entry.ok <- true
	}
}

type Entries []Entry

func (this Entries) Len() int {
	return len(this)
}

func (this Entries) Swap(i, j int) {
	this[i], this[j] = this[j], this[i]
}

func (this Entries) Less(i, j int) bool {
	return !(this[i].Points < this[j].Points) // Descending order
}
