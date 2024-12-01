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

func parsing() ([]int, []int) {
	dat, err := os.ReadFile("../../day/1/input.txt")
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

	return a, b
}

func part1() int {
	left, right := parsing()
	slices.Sort(left)
	slices.Sort(right)

	total := 0

	for i := range len(left) {
		x := left[i] - right[i]
		if x < 0 {
			x = -x
		}
		total += x
	}
	return total
}

func part2() int {

	left, right := parsing()

	m := make(map[int]int)
	n := make(map[int]int)

	for i := range len(left) {
		m[left[i]] += 1
		n[right[i]] += 1
	}

	total := 0
	for k, v := range m {
		total += k * v * n[k]
	}

	return total
}

func main() {
	fmt.Println("Part 1 answer: " + strconv.Itoa(part1()))
	fmt.Println("Part 2 answer: " + strconv.Itoa(part2()))
}
