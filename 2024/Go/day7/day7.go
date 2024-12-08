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

func parsing() map[int][]int {
	file, err := os.Open("../../day/7/input.txt")
	check(err)
	defer file.Close()

	a := make(map[int][]int)
	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		equations := scanner.Text()
		calibration := strings.Split(equations, ":")

		b := make([]int, 0)
		for _, c := range strings.Fields(calibration[1]) {
			num, err := strconv.Atoi(c)
			check(err)
			b = append(b, num)
		}
		num, err := strconv.Atoi(calibration[0])
		check(err)
		a[num] = b
	}
	return a
}

func solved(sol int, curr int, data []int) bool {
	if curr > sol {
		return false
	}
	if len(data) == 0 {
		return sol == curr
	}
	return solved(sol, curr+data[0], data[1:]) || solved(sol, curr*data[0], data[1:])
}

func solvedMore(sol int, curr int, data []int) bool {
	if curr > sol {
		return false
	}
	if len(data) == 0 {
		return sol == curr
	}
	result := strconv.Itoa(curr) + strconv.Itoa(data[0])
	//fmt.Println(result, curr, data[0])
	num, err := strconv.Atoi(result)
	check(err)
	return solvedMore(sol, curr+data[0], data[1:]) ||
		solvedMore(sol, curr*data[0], data[1:]) ||
		solvedMore(sol, num, data[1:])
}

func part1() int {
	data := parsing()
	total := 0
	for sol := range data {
		if solved(sol, data[sol][0], data[sol][1:]) {
			total += sol
		}
	}

	return total
}

func part2() int {
	data := parsing()
	total := 0
	for sol := range data {
		if solvedMore(sol, data[sol][0], data[sol][1:]) {
			total += sol
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
