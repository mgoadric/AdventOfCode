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

func parsing() [][]byte {
	dat, err := os.ReadFile("../../day/6/input.txt")
	check(err)

	grid := bytes.Split(dat, []byte("\n"))
	return grid[:len(grid)-1]

}

type point struct {
	x int
	y int
}

func part1() int {

	grid := parsing()

	height := len(grid)
	width := len(grid[0])
	guard := point{x: -1, y: -1}
	dirs := map[rune]point{
		'^': {x: -1, y: 0},
		'>': {x: 0, y: 1},
		'v': {x: 1, y: 0},
		'<': {x: 0, y: -1},
	}
	dnext := []rune{
		'^', '>', 'v', '<',
	}
	d := 0

	for i := range height {
		//fmt.Println(grid[i])
		found := false
		for j := range width {
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
		}
	}

	return total
}

func part2() int {

	//grid := parsing()

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
