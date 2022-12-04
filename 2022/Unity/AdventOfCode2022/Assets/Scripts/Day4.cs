using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Day4 : MonoBehaviour
{

    Boolean FullyContains(Tuple<int, int> elf1, Tuple<int, int>elf2) {
        return elf1.Item1 <= elf2.Item1 && elf1.Item2 >= elf2.Item2;
    }

    Boolean Overlap(Tuple<int, int> elf1, Tuple<int, int>elf2) {
        return !(elf1.Item1 > elf2.Item2 || elf1.Item2 < elf2.Item1);
    }
    // Start is called before the first frame update
    void Start()
    {
        string path = "Assets/Data/input4.txt";

        //Read the text from directly from the file
        StreamReader reader = new StreamReader(path); 

        List<List<Tuple<int, int>>> data = reader.ReadToEnd().Split("\n").Select(
            pair => pair.Split(",").Select(
                elf => new Tuple<int, int>(int.Parse(elf.Substring(0, elf.IndexOf("-"))), 
                int.Parse(elf.Substring(1 + elf.IndexOf("-"))))
            ).ToList()).ToList();
        reader.Close();

        Debug.Log(data.Count());

        foreach (List<Tuple<int, int>> pair in data) {
            foreach(Tuple<int, int> elf in pair) {
                //Debug.Log(elf.Item1 + " -> " + elf.Item2);
            }
        }

        int count = 0;
        foreach (List<Tuple<int, int>> pair in data) {
            if (FullyContains(pair[0], pair[1]) || 
                FullyContains(pair[1], pair[0])) {
                    count++;
            }
        }
        Debug.Log("Part 1: " + count);

        int count2 = 0;
        foreach (List<Tuple<int, int>> pair in data) {
            if (Overlap(pair[0], pair[1])) {
                    count2++;
            }
        }
        Debug.Log("Part 2: " + count2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
