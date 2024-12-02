package main

import (
	"bufio"
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

func parsing() [][]int {
	file, err := os.Open("../../day/2/input.txt")
	check(err)
	defer file.Close()

	a := make([][]int, 0)
	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		words := strings.Fields(scanner.Text())
		//fmt.Println(words)
		b := make([]int, 0)
		for _, w := range words {
			num, _ := strconv.Atoi(w)
			b = append(b, num)
		}
		a = append(a, b)
	}
	return a
}

func desc(a int, b int) bool {
	return b < a
}

func asc(a int, b int) bool {
	return a < b
}

func gradual(a int, b int) bool {
	x := a - b
	if x < 0 {
		x = -x
	}
	return x >= 1 && x <= 3
}

func safe(r []int) bool {
	a := true
	d := true
	g := true
	for i := range r {
		if !a && !d {
			break
		}
		if i > 0 {
			a = a && asc(r[i-1], r[i])
			d = d && desc(r[i-1], r[i])
			g = g && gradual(r[i-1], r[i])
		}
	}
	return g && (a || d)

}

func shrink(r []int, i int) []int {
	t := make([]int, len(r)-1)
	for j := range len(r) {
		if j < i {
			t[j] = r[j]
		} else if j > i {
			t[j-1] = r[j]
		}
	}
	return t
}

func part1() int {
	reports := parsing()

	total := 0
	for _, r := range reports {
		if safe(r) {
			total++
		}
	}
	return total
}

func part2() int {
	reports := parsing()

	total := 0
	for _, r := range reports {
		s := safe(r)
		for i := range r {
			if s {
				break
			}
			t := shrink(r, i)
			s = s || safe(t)
		}
		if s {
			total++
		}
	}
	return total
}

func main() {
	start := time.Now()
	fmt.Println("Part 1 answer: " + strconv.Itoa(part1()))
	elapsed := time.Since(start)
	fmt.Printf("took %s\n", elapsed)

	start = time.Now()
	fmt.Println("Part 2 answer: " + strconv.Itoa(part2()))
	elapsed = time.Since(start)
	fmt.Printf("took %s\n", elapsed)
}
