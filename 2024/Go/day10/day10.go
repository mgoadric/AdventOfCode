package main

import (
	"fmt"
	"os"
	"slices"
	"strconv"
	"strings"
	"time"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func parsing() []int {

	data, err := os.ReadFile("../../day/10/input.txt")
	check(err)

	s := make([]int, 0)
	calibration := strings.Fields(string(data))
	for _, p := range calibration {
		num, err := strconv.Atoi(p)
		check(err)
		s = append(s, num)
	}

	return s
}

func part1() int {
	stones := parsing()
	//fmt.Println(0, stones)

	for range 25 {

		for j := 0; j < len(stones); j++ {
			t := strconv.Itoa(stones[j])
			if stones[j] == 0 {
				stones[j] = 1
			} else if len(t)%2 == 0 {
				b, err := strconv.Atoi(t[:len(t)/2])
				check(err)
				c, err := strconv.Atoi(t[len(t)/2:])
				check(err)
				stones[j] = c
				stones = slices.Insert(stones, j, b)
				j++
			} else {
				stones[j] *= 2024
			}
		}

		//fmt.Println(i+1, stones)
	}
	return len(stones)
}

func part2() int {
	stones := parsing()
	//fmt.Println(0, stones)

	// put in dictionary??

	for i := range 75 {

		for j := 0; j < len(stones); j++ {
			t := strconv.Itoa(stones[j])
			if stones[j] == 0 {
				stones[j] = 1
			} else if len(t)%2 == 0 {
				b, err := strconv.Atoi(t[:len(t)/2])
				check(err)
				c, err := strconv.Atoi(t[len(t)/2:])
				check(err)
				stones[j] = c
				stones = slices.Insert(stones, j, b)
				j++
			} else {
				stones[j] *= 2024
			}
		}

		//fmt.Println(i+1, stones)
		fmt.Println(i, len(stones))
	}
	return len(stones)
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
