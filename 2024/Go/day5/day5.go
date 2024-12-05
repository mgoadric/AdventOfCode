package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
	"time"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func parsing() (map[int][]int, [][]int) {
	dat, err := os.ReadFile("../../day/5/input.txt")
	check(err)

	temp := strings.Split(string(dat), "\n\n")

	os := strings.Split(temp[0], "\n")
	orderings := make(map[int][]int)
	for _, order := range os {
		pages := strings.Split(order, "|")
		fmt.Println(pages)
	}

	us := strings.Split(temp[1], "\n")
	updates := make([][]int, 0)
	for _, s := range us {
		if len(s) == 0 {
			break
		}
		b := make([]int, 0)
		ps := strings.Split(s, ",")
		for _, p := range ps {
			page, err := strconv.Atoi(p)
			check(err)
			b = append(b, page)
		}

		updates = append(updates, b)
	}

	return orderings, updates

}

func part1() int {
	orders, updates := parsing()
	fmt.Println(orders)

	fmt.Println(updates[(len(updates) - 1)])
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
