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

func parsing() []map[rune]point {
	dat, err := os.ReadFile("../../day/13/input.txt")
	check(err)

	m := make([]map[rune]point, 0)

	ms := strings.Split(string(dat), "\n\n")
	for _, mach := range ms {
		d := map[rune]point{}
		pieces := strings.Split(mach, "\n")

		axy := strings.Split(pieces[0][10:], ", ")
		x, err := strconv.Atoi(axy[0][1:])
		check(err)
		y, err := strconv.Atoi(axy[1][1:])
		check(err)
		d['A'] = point{x: x, y: y}

		axy = strings.Split(pieces[1][10:], ", ")
		x, err = strconv.Atoi(axy[0][1:])
		check(err)
		y, err = strconv.Atoi(axy[1][1:])
		check(err)
		d['B'] = point{x: x, y: y}

		axy = strings.Split(pieces[2][7:], ", ")
		x, err = strconv.Atoi(axy[0][2:])
		check(err)
		y, err = strconv.Atoi(axy[1][2:])
		check(err)
		d['P'] = point{x: x, y: y}

		m = append(m, d)
	}
	return m
}

type point struct {
	x int
	y int
}

func (p point) eq(p2 point) bool {
	return p.x == p2.x && p.y == p2.y
}

func (p point) add(p2 point) point {
	return point{x: p.x + p2.x, y: p.y + p2.y}
}

func (p point) mult(m int) point {
	return point{x: p.x * m, y: p.y * m}
}

func mash(m map[rune]point) int {
	s := 401
	for i := range 100 {
		for j := range 100 {
			if m['A'].mult(i).add(m['B'].mult(j)).eq(m['P']) {
				c := 3*i + j
				if c < s {
					s = c
				}
			}
		}
	}
	if s == 401 {
		s = 0
	}
	return s
}

func mash2(m map[rune]point) int {
	s := 401
	for i := range 100 {
		for j := range 100 {
			if m['A'].mult(i).add(m['B'].mult(j)).eq(m['P']) {
				c := 3*i + j
				if c < s {
					s = c
				}
			}
		}
	}
	if s == 401 {
		s = 0
	}
	return s
}

func part1() int {
	data := parsing()

	total := 0
	for _, m := range data {
		total += mash(m)
	}
	return total
}

func part2() int {
	data := parsing()

	total := 0
	for _, m := range data {
		total += mash2(m)
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
