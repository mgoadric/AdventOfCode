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

func parsing() ([][]byte, point) {
	dat, err := os.ReadFile("../../day/6/input.txt")
	check(err)

	grid := bytes.Split(dat, []byte("\n"))
	grid = grid[:len(grid)-1]

	guard := point{x: -1, y: -1}

	for i := range len(grid) {
		//fmt.Println(grid[i])
		found := false
		for j := range len(grid[i]) {
			if grid[i][j] == '^' {
				guard.x = i
				guard.y = j
				found = true
				break
			}
		}
		if found {
			break
		}
	}
	return grid, guard
}

func gridPrint(grid [][]byte) {
	for i := range len(grid) {
		fmt.Println(string(grid[i]))
	}
}

type point struct {
	x int
	y int
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

func patrol(grid [][]byte, guard point) int {
	height := len(grid)
	width := len(grid[0])

	d := 0
	total := 1

	for {
		// Bumping into objects
		for {
			next := point{x: guard.x + dirs[dnext[d]].x, y: guard.y + dirs[dnext[d]].y}
			if next.x >= height || next.y >= width || next.x < 0 || next.y < 0 {
				guard = next
				break
			}
			if grid[next.x][next.y] == '#' {
				d++
				d %= len(dirs)
			} else {
				guard = next
				break
			}
		}

		// Out of bounds? Stop!
		if guard.x >= height || guard.y >= width || guard.x < 0 || guard.y < 0 {
			break
		}

		// Is this somewhere new?
		_, ok := dirs[rune(grid[guard.x][guard.y])]
		if !ok {
			grid[guard.x][guard.y] = byte(dnext[d])
			total++
		} else {
			if rune(grid[guard.x][guard.y]) == dnext[d] {
				total = -1
				break
			}
		}
	}
	return total
}

func part1() int {

	grid, guard := parsing()
	return patrol(grid, guard)
}

func part2() int {

	grid, guard := parsing()

	total := 0

	for i := range len(grid) {
		for j := range len(grid[i]) {
			mygrid := make([][]byte, len(grid))
			for a := range len(grid) {
				row := make([]byte, len(grid[i]))
				for b := range len(grid[a]) {
					row[b] = grid[a][b]
				}
				mygrid[a] = row
			}
			gpos := point{x: guard.x, y: guard.y}

			if gpos.x != i || gpos.y != j {
				mygrid[i][j] = '#'
				if patrol(mygrid, gpos) == -1 {
					total++
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
