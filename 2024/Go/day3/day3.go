package main

import (
	"fmt"
	"os"
	"time"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func parsing() [][]int {
	file, err := os.Open("../../day/3/input.txt")
	check(err)
	defer file.Close()

	a := make([][]int, 0)

	return a
}

func part1() int {
	//data := parsing()

	total := 0

	return total
}

func part2() int {
	//data := parsing()

	total := 0

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
