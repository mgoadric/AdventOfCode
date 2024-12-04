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
	dat, err := os.ReadFile("../../day/4/input.txt")
	check(err)

	return strings.Split(string(dat), "\n")
}

type point struct {
	x int
	y int
}

func part1() int {
	data := parsing()

	size := 140
	dirs := []point{
		{x: 1, y: 0},
		{x: -1, y: 0},
		{x: 0, y: 1},
		{x: 0, y: -1},
		{x: 1, y: 1},
		{x: -1, y: 1},
		{x: 1, y: -1},
		{x: -1, y: -1},
	}

	target := "XMAS"

	total := 0
	for i := range size {
		for j := range size {
			if data[i][j] == 'X' {
				for _, d := range dirs {
					spot := point{x: i, y: j}
					found := 1
					for {
						if found == len(target) {
							break
						}
						spot.x += d.x
						spot.y += d.y
						if spot.x >= size || spot.y >= size || spot.x < 0 || spot.y < 0 {
							break
						}
						if data[spot.x][spot.y] != target[found] {
							break
						}
						found++
					}
					if found == len(target) {
						total += 1
					}
				}
			}
		}
	}

	return total
}

func part2() int {
	data := parsing()

	size := 140
	dirs := []point{
		{x: 1, y: 1},
		{x: -1, y: 1},
		{x: -1, y: -1},
		{x: 1, y: -1},
	}

	target := "MMSS"

	total := 0
	for i := range size {
		for j := range size {
			if data[i][j] == 'A' {
				for n := range dirs {
					found := 0
					for {
						if found == len(target) {
							break
						}
						spot := point{x: i + dirs[n].x, y: j + dirs[n].y}
						if spot.x >= size || spot.y >= size || spot.x < 0 || spot.y < 0 {
							break
						}
						if data[spot.x][spot.y] != target[found] {
							break
						}
						found++
						n++
						n %= len(dirs)
					}
					if found == len(target) {
						total += 1
					}
				}
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
