package main

import (
	"fmt"
	"os"
	"time"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

type chunk struct {
	i     int
	id    int
	len   int
	space int
}

func parsing() ([]chunk, []int) {

	data, err := os.ReadFile("../../day/9/input.txt")
	check(err)
	//fmt.Print(string(data))

	d := make([]chunk, 0)
	s := make([]int, 0)

	data = data[:len(data)-1]

	loc := 0
	for i := 0; i < len(data); i++ {
		v := int(data[i]) - 48
		//fmt.Print(v)
		if i%2 == 0 {
			d = append(d, chunk{i: loc, id: i / 2, len: v})
		} else {
			s = append(s, v)
		}
		loc += v
	}

	return d, s
}

func parsing2() []chunk {

	data, err := os.ReadFile("../../day/9/input.txt")
	check(err)
	//fmt.Print(string(data))

	d := make([]chunk, 0)

	data = data[:len(data)-1]

	loc := 0
	for i := 0; i < len(data); i++ {
		//fmt.Println(i, int(data[i])-48)
		v := int(data[i]) - 48
		if i%2 == 0 {
			sp := 0
			if i+1 < len(data) {
				sp = int(data[i+1]) - 48
			}
			d = append(d, chunk{i: loc, id: i / 2, len: v, space: sp})
		}
		loc += v
	}

	return d
}

func calc(d chunk) int {
	t := 0
	for i := range d.len {
		t += d.id * (d.i + i)
		//fmt.Println(d.id, d.i+i, d.id*(d.i+i))
	}
	return t
}

func part1() int {
	data, spaces := parsing()

	total := 0
	for {
		//fmt.Println(data, spaces)

		// Process data chunk
		if len(data) == 0 {
			break
		}
		d := data[0]
		total += calc(d)
		data = data[1:]

		if len(data) == 0 {
			break
		}

		// Fill in spaces
		e := data[len(data)-1]
		//fmt.Println("d:", d)
		//fmt.Println("e:", e)
		used := 0
		for {
			if len(spaces) == 0 {
				break
			}
			if spaces[0] < e.len {
				data[len(data)-1].len -= spaces[0]
				t := chunk{i: d.i + d.len + used, id: e.id, len: spaces[0]}
				total += calc(t)
				break
			} else {
				spaces[0] -= e.len
				e.i = d.i + d.len + used
				total += calc(e)
				used += e.len
				data = data[:len(data)-1]
				e = data[len(data)-1]
			}
		}
		spaces = spaces[1:]

	}
	return total
}

func part2() int {
	data := parsing2()

	//fmt.Println(data)
	total := 0
	for {
		//fmt.Println(data)

		// Process data chunk
		if len(data) == 0 {
			break
		}
		d := data[0]
		total += calc(d)
		data = data[1:]

		if len(data) == 0 {
			break
		}

		used := 0
		for {
			if len(data) == 0 {
				break
			} else {
				e := data[len(data)-1]
				// Fill in spaces
				//fmt.Println("d:", d)
				//fmt.Println("e:", e)

				if d.space < e.len {
					data[len(data)-1].len -= d.space
					t := chunk{i: d.i + d.len + used, id: e.id, len: d.space}
					total += calc(t)
					break
				} else {
					d.space -= e.len
					e.i = d.i + d.len + used
					total += calc(e)
					used += e.len
					data = data[:len(data)-1]
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
