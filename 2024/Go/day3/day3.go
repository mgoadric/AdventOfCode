package main

import (
	"fmt"
	"os"
	"regexp"
	"slices"
	"strconv"
	"time"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func parsing() []byte {
	dat, err := os.ReadFile("../../day/3/input.txt")
	check(err)

	return dat
}

func part1() int {
	data := parsing()

	r, err := regexp.Compile("mul[(]([0-9]+),([0-9]+)[)]")
	check(err)

	match := r.FindAllSubmatch(data, -1)

	total := 0
	for _, m := range match {
		//fmt.Println(string(m[0]))
		a, _ := strconv.Atoi(string(m[1]))
		b, _ := strconv.Atoi(string(m[2]))
		total += a * b
	}

	return total
}

func part2() int {
	data := parsing()

	r, err := regexp.Compile("mul[(]([0-9]+),([0-9]+)[)]")
	check(err)

	match := r.FindAllSubmatch(data, -1)
	matchi := r.FindAllIndex(data, -1)

	d, err := regexp.Compile("do[(][)]")
	check(err)

	dmatchi := d.FindAllIndex(data, -1)

	n, err := regexp.Compile(regexp.QuoteMeta("don't") + "[(][)]")
	check(err)

	nmatchi := n.FindAllIndex(data, -1)

	basket := make(map[int]int)
	for i, m := range matchi {
		basket[m[0]] = i
	}
	for _, m := range dmatchi {
		basket[m[0]] = -1
	}
	for _, m := range nmatchi {
		basket[m[0]] = -2
	}

	// https://www.geeksforgeeks.org/how-to-sort-golang-map-by-keys-or-values/
	keys := make([]int, 0, len(basket))

	for k := range basket {
		keys = append(keys, k)
	}
	slices.Sort(keys)

	total := 0
	enabled := true
	for _, k := range keys {
		if basket[k] == -1 {
			enabled = true
		} else if basket[k] == -2 {
			enabled = false
		} else {
			if enabled {
				a, _ := strconv.Atoi(string(match[basket[k]][1]))
				b, _ := strconv.Atoi(string(match[basket[k]][2]))
				total += a * b
			}
		}
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
