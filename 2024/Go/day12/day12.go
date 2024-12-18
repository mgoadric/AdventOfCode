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
	points  []point
	fences  map[point]bool
	corners map[point]int
}

func (b *blob) add(p point) {
	b.points = append(b.points, p)
}

func (b blob) area() int {
	return len(b.points)
}

func (b blob) perimeter() int {
	nudge := point{x: 1, y: 1}
	f := make(map[point]bool)
	for _, bp := range b.points {
		for _, d := range dirNext {
			pp := bp.mult(2).add(dirs[d]).add(nudge)
			if f[pp] {
				delete(f, pp)
			} else {
				f[pp] = true
			}
		}
	}
	return len(f)
}

func (b blob) bulk() int {
	nudge := point{x: 1, y: 1}
	f := make(map[point]bool)
	c := make(map[point]int)

	for _, bp := range b.points {
		for i, d := range dirNext {
			fp := bp.mult(2).add(dirs[d]).add(nudge)
			if f[fp] {
				delete(f, fp)
			} else {
				f[fp] = true
			}
			cp := bp.mult(2).add(dirs[dirNext[i]]).add(dirs[dirNext[(i+1)%len(dirNext)]]).add(nudge)
			if c[cp] == 3 {
				delete(c, cp)
			} else {
				c[cp]++
			}
		}
	}

	path := make(map[point]map[rune]point)
	edges := 0
	for cp := range c {
		path[cp] = make(map[rune]point)
		for _, d := range dirNext {
			if f[cp.add(dirs[d])] {
				path[cp][d] = cp.add(dirs[d].mult(2))
			}
		}
		if len(path[cp]) == 4 {
			edges += 2
		} else {
			for d := range path[cp] {
				_, ok := path[cp][dirOpp[d]]
				if !ok {
					edges += 1
					break
				}
			}
		}
	}
	return edges
}

// //// DEBUG PRINTING
func (b blob) perimeterP() map[point]bool {
	p := make(map[point]bool)
	for _, bp := range b.points {
		for _, d := range dirNext {
			pp := bp.mult(2).add(dirs[d]).add(point{x: 1, y: 1})
			if p[pp] {
				delete(p, pp)
			} else {
				p[pp] = true
			}
		}
	}
	return p
}

func (b blob) bulkP() map[point]int {
	p := make(map[point]int)
	for _, bp := range b.points {
		for i := range dirNext {
			pp := bp.mult(2).add(dirs[dirNext[i]]).add(dirs[dirNext[(i+1)%len(dirNext)]]).add(point{x: 1, y: 1})
			if p[pp] == 3 {
				delete(p, pp)
			} else {
				p[pp]++
			}
		}
	}
	return p
}

func blobPrint(fences map[point]bool, corners map[point]int, bound point) {
	for i := range bound.x*2 + 1 {
		for j := range bound.y*2 + 1 {
			p := point{x: i, y: j}
			c, ok := corners[p]
			if fences[p] && ok {
				panic(1)
			}

			if fences[p] {
				fmt.Print("#")
			} else if ok {
				fmt.Print(c)
			} else {
				fmt.Print(" ")
			}
		}
		fmt.Println()
	}
}

////// END OF DEBUG SECTION

var dirs = map[rune]point{
	'^': {x: -1, y: 0},
	'>': {x: 0, y: 1},
	'v': {x: 1, y: 0},
	'<': {x: 0, y: -1},
}
var dirNext = []rune{
	'^', '>', 'v', '<',
}

var dirOpp = map[rune]rune{
	'^': 'v',
	'>': '<',
	'v': '^',
	'<': '>',
}

func findBlob(grid [][]byte, bounds point, p point, v map[point]bool) blob {

	q := make([]point, 1)
	q[0] = p

	b := blob{}

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

		for _, d := range dirNext {
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

	bounds := point{x: len(grid), y: len(grid[0])}

	//gridPrint(grid)

	v := make(map[point]bool)

	total := 0
	for i := range bounds.x {
		for j := range bounds.y {
			p := point{x: i, y: j}
			if !v[p] {
				b := findBlob(grid, bounds, p, v)
				//blobPrint(b.perimeterP(), b.bulkP(), bounds)
				//fmt.Println(b.area(), b.perimeter(), b.bulk())
				total += b.area() * b.bulk()
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
