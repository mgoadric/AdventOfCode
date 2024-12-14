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
	dat, err := os.ReadFile("../../day/12/input.txt")
	check(err)

	grid := bytes.Split(dat, []byte("\n"))
	grid = grid[:len(grid)-1]

	return grid
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

func (p point) add(p2 point) point {
	return point{x: p.x + p2.x, y: p.y + p2.y}
}

func (p point) mult(m int) point {
	return point{x: p.x * m, y: p.y * m}
}

func (p point) inRange(bounds point) bool {
	return p.x >= 0 && p.y >= 0 && p.x < bounds.x && p.y < bounds.y
}

type blob struct {
	points []point
}

func (b *blob) add(p point) {
	b.points = append(b.points, p)
}

func (b blob) area() int {
	return len(b.points)
}

func (b blob) perimeter() int {
	p := make(map[point]bool)
	for _, bp := range b.points {
		for _, d := range dnext {
			pp := bp.mult(2).add(dirs[d])
			if p[pp] {
				delete(p, pp)
			} else {
				p[pp] = true
			}
		}
	}
	return len(p)
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

func findBlob(grid [][]byte, bounds point, p point, v map[point]bool) blob {

	q := make([]point, 1)
	q[0] = p

	ps := make([]point, 0)
	b := blob{points: ps}

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

		b.add(t)
		v[t] = true

		for _, d := range dnext {
			t2 := t.add(dirs[d])

			// Out of bounds?
			if !t2.inRange(bounds) {
				continue
			}
			if grid[t2.x][t2.y] == grid[p.x][p.y] {
				//fmt.Println(string(d), t2)
				if !v[t2] {
					q = append(q, t2)
				}
			}
		}
	}

	return b
}

func part1() int {

	grid := parsing()

	bounds := point{x: len(grid), y: len(grid[0])}

	//gridPrint(grid)

	v := make(map[point]bool)

	total := 0
	for i := range bounds.x {
		for j := range bounds.y {
			p := point{x: i, y: j}
			if !v[p] {
				b := findBlob(grid, bounds, p, v)
				//fmt.Println(b, b.area(), b.perimeter())
				total += b.area() * b.perimeter()
			}
		}
	}
	return total

}

func part2() int {

	grid := parsing()

	return len(grid)
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
