package main

import (
	"bytes"
	"fmt"
	"os"
	"time"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func parsing() (map[byte][]point, point) {

	dat, err := os.ReadFile("../../day/8/input.txt")
	check(err)

	grid := bytes.Split(dat, []byte("\n"))
	grid = grid[:len(grid)-1]

	stations := make(map[byte][]point)

	for i := range len(grid) {
		//fmt.Println(grid[i])
		for j := range len(grid[i]) {
			if grid[i][j] != '.' {
				p := point{x: i, y: j}
				_, ok := stations[grid[i][j]]
				if !ok {
					s := make([]point, 0)
					stations[grid[i][j]] = s
				}
				stations[grid[i][j]] = append(stations[grid[i][j]], p)
			}
		}
	}
	return stations, point{x: len(grid), y: len(grid[0])}
}

type point struct {
	x int
	y int
}

func (p point) sub(p2 point) point {
	return point{x: p.x - p2.x, y: p.y - p2.y}
}

func (p point) add(p2 point) point {
	return point{x: p.x + p2.x, y: p.y + p2.y}
}

func (p point) inv() point {
	return point{x: -p.x, y: -p.y}
}

func (p point) inRange(bounds point) bool {
	return p.x >= 0 && p.y >= 0 && p.x < bounds.x && p.y < bounds.y
}

func (p point) less(p2 point) bool {
	return p.x < p2.x && p.y < p2.y
}

func part1() int {
	stations, dim := parsing()
	//fmt.Println(stations, dim)

	nodes := make(map[point]bool)
	for s := range stations {
		for i := range len(stations[s]) {
			for j := i + 1; j < len(stations[s]); j++ {
				a := stations[s][i]
				b := stations[s][j]
				if b.less(a) {
					b, a = a, b
				}

				diff := a.sub(b)
				first := a.add(diff)
				if first.inRange(dim) {
					//fmt.Println(first)
					nodes[first] = true
				}
				second := b.add(diff.inv())
				if second.inRange(dim) {
					//fmt.Println(second)
					nodes[second] = true
				}
			}
		}
	}

	return len(nodes)
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
