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

func parsing() [][]int {
	dat, err := os.ReadFile("../../day/10/input.txt")
	check(err)

	grid := bytes.Split(dat, []byte("\n"))
	grid = grid[:len(grid)-1]

	a := make([][]int, len(grid))
	for i := range len(grid) {
		b := make([]int, len(grid[i]))
		a[i] = b
		for j := range len(grid[i]) {
			a[i][j] = int(grid[i][j]) - 48
		}
	}
	return a
}

func gridPrint(grid [][]int) {
	for i := range len(grid) {
		for j := range len(grid[i]) {
			fmt.Print(grid[i][j])
		}
		fmt.Println()
	}
}

type point struct {
	x     int
	y     int
	level int
}

func (p point) add(p2 point) point {
	return point{x: p.x + p2.x, y: p.y + p2.y}
}

var dirs = map[rune]point{
	'^': {x: -1, y: 0},
	'>': {x: 0, y: 1},
	'v': {x: 1, y: 0},
	'<': {x: 0, y: -1},
}
var dnext = []rune{
	'^', '>', 'v', '<',
}

func walk(grid [][]int, p point, record bool) int {

	height := len(grid)
	width := len(grid[0])
	total := 0
	q := make([]point, 1)
	q[0] = p

	v := make(map[point]bool)

	for {
		if len(q) == 0 {
			break
		}
		t := q[0]
		q = q[1:]

		// Already seen
		if v[t] {
			continue
		}
		if record {
			v[t] = true
		}

		if grid[t.x][t.y] == 9 {
			total += 1
			continue
		}

		for _, d := range dnext {
			t2 := t.add(dirs[d])

			// Out of bounds?
			if t2.x >= height || t2.y >= width || t2.x < 0 || t2.y < 0 {
				continue
			}
			if grid[t2.x][t2.y]-1 == t.level {
				t2.level = t.level + 1
				//fmt.Println(string(d), t2)
				q = append(q, t2)
			}
		}
	}

	return total
}

func part1() int {

	grid := parsing()
	//gridPrint(grid)

	total := 0
	for i := range len(grid) {
		for j := range len(grid[i]) {
			if grid[i][j] == 0 {
				t := point{x: i, y: j}
				//fmt.Println(t)
				total += walk(grid, t, true)
			}
		}
	}

	return total
}

func part2() int {

	grid := parsing()
	//gridPrint(grid)

	total := 0
	for i := range len(grid) {
		for j := range len(grid[i]) {
			if grid[i][j] == 0 {
				t := point{x: i, y: j}
				//fmt.Println(t)
				total += walk(grid, t, false)
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
