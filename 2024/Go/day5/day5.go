package main

import (
	"fmt"
	"os"
	"strings"
	"time"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func parsing() []string {
	dat, err := os.ReadFile("../../day/5/input.txt")
	check(err)

	return strings.Split(string(dat), "\n")
}

func part1() int {
	//data := parsing()

	total := 0

	for {
		break
	}

	return total
}

func part2() int {
	//data := parsing()

	total := 0
	for {
		break
	}

	return total
}

func main() {
	start := time.Now()
	fmt.Printf("Part 1 answer: %d\n", part1())
	elapsed := time.Since(start)
	fmt.Printf("took %s\n", elapsed)

	start = time.Now()
	fmt.Printf("Part 2 answer: %d\n", part2())
	elapsed = time.Since(start)
	fmt.Printf("took %s\n", elapsed)
}
