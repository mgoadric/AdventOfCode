package main

import (
	"fmt"
	"os"
	"strconv"
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
	fmt.Println("Part 1 answer: " + strconv.Itoa(part1()))
	elapsed := time.Since(start)
	fmt.Printf("took %s\n", elapsed)

	start = time.Now()
	fmt.Println("Part 2 answer: " + strconv.Itoa(part2()))
	elapsed = time.Since(start)
	fmt.Printf("took %s\n", elapsed)
}
