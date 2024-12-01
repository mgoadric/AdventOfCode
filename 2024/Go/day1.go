package main

import (
	"fmt"
	"os"
	"slices"
	"strconv"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func part1() int {

	dat, err := os.ReadFile("../day/1/input.txt")
	check(err)

	a := make([]int, 0)
	b := make([]int, 0)

	for j := 0; j < len(dat); j += 14 {
		first_s := string(dat[j : j+5])
		first, _ := strconv.Atoi(first_s)
		a = append(a, first)

		second_s := string(dat[j+8 : j+13])
		second, _ := strconv.Atoi(second_s)
		b = append(b, second)
	}

	slices.Sort(a)
	slices.Sort(b)

	total := 0

	for i := range len(a) {
		x := a[i] - b[i]
		if x < 0 {
			x = -x
		}
		total += x
	}
	return total
}

func main() {
	fmt.Println("Part 1 answer: " + strconv.Itoa(part1()))
}
