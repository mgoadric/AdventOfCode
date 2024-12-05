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

func parsing() (map[int]map[int]bool, [][]int) {
	dat, err := os.ReadFile("../../day/5/input.txt")
	check(err)

	temp := strings.Split(string(dat), "\n\n")

	os := strings.Split(temp[0], "\n")
	orderings := make(map[int]map[int]bool)
	for _, order := range os {
		pages := strings.Split(order, "|")
		before, err := strconv.Atoi(pages[0])
		check(err)
		after, err := strconv.Atoi(pages[1])
		check(err)
		afters, found := orderings[before]
		if !found {
			afters = make(map[int]bool)
			orderings[before] = afters
		}
		afters[after] = true
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
	orderings, updates := parsing()

	total := 0

	for _, u := range updates {
		correct := true
		for i := 0; i < len(u); i++ {
			for j := 0; j < i; j++ {
				t, ok := orderings[u[i]]
				if ok {
					if t[u[j]] {
						correct = false
						//fmt.Println("Aha!")
						break
					}
				}
			}
		}
		if correct {
			total += u[len(u)/2]
		}
	}

	return total
}

func part2() int {
	orderings, updates := parsing()

	total := 0

	for _, u := range updates {
		fixed := false
		for i := 0; i < len(u); i++ {
			for j := 0; j < i; j++ {
				t, ok := orderings[u[i]]
				if ok {
					if t[u[j]] {
						fixed = true
						temp := u[i]
						u[i] = u[j]
						u[j] = temp
						i = j
						//fmt.Println("Aha!")
						break
					}
				}
			}
		}
		if fixed {
			total += u[len(u)/2]
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
