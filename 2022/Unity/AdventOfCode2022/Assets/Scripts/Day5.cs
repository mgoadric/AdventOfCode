using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class Day5 : MonoBehaviour
{
   
    public bool sample = false;

    private List<Tuple<int, int, int>> data;

   //private string[] starting = {"ZN", "MCD", "P"};

/*
        [Q] [B]         [H]        
    [F] [W] [D] [Q]     [S]        
    [D] [C] [N] [S] [G] [F]        
    [R] [D] [L] [C] [N] [Q]     [R]
[V] [W] [L] [M] [P] [S] [M]     [M]
[J] [B] [F] [P] [B] [B] [P] [F] [F]
[B] [V] [G] [J] [N] [D] [B] [L] [V]
[D] [P] [R] [W] [H] [R] [Z] [W] [S]
 1   2   3   4   5   6   7   8   9 
*/

    private string[] starting = {"DBJV",
    "PVBWRDF", "RGFLDCWQ", "WJPMLNDB", "HNBPCSQ", "RDBSNG", "ZBPMQFSH", "WLF", "SVFMR"};

    private void LoadData(string filename) {

        //Read the text from directly from the input.txt file
        StreamReader reader = new StreamReader(filename); 
        List<string> lines = reader.ReadToEnd().Split("\n").ToList();
        reader.Close();
        
        data = new List<Tuple<int, int, int>>();
        // https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.match.groups?view=net-7.0
        string pat = @"move (\d+) from (\d+) to (\d+)";
        foreach (string line in lines) {
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);
            Match m = r.Match(line);
            Debug.Log(m.Groups[1].Value + ", " + m.Groups[2].Value + ", " + m.Groups[3].Value);
            data.Add(new Tuple<int, int, int>(int.Parse(m.Groups[1].Value), 
                int.Parse(m.Groups[2].Value), 
                int.Parse(m.Groups[3].Value)));
        }

        Debug.Log(data.Count());
    }

    // Start is called before the first frame update
    void Start()
    {
        string path = "Assets/Data/";
        if (sample) {
            path += "sample";
        }
        LoadData(path + "input5.txt");
        
        List<Stack<char>> stacks = new List<Stack<char>>();
        foreach (string initial in starting) {
            Stack<char> s = new Stack<char>(initial.ToArray());
            Debug.Log(s.Peek());
            stacks.Add(s);
        }

        foreach (Tuple<int, int, int> instruction in data) {
            for (int i = 0; i < instruction.Item1; i++) {
                stacks[instruction.Item3 - 1].Push(stacks[instruction.Item2 - 1].Pop());
            }
        }

        string tops = "";
        foreach (Stack<char> s in stacks) {
            tops += s.Peek();
        }
        Debug.Log("Part 1: " + tops);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
