using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Day5 : MonoBehaviour
{
   
    private bool sample = false;

    public GameObject cratePrefab;

    private List<Tuple<int, int, int>> data;

    public List<Stack<GameObject>> crateStacks = new List<Stack<GameObject>>();



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

    //private string[] starting = {"ZN", "MCD", "P"};

    private string[] starting = {"DBJV", "PVBWRDF", "RGFLDCWQ", "WJPMLNDB", "HNBPCSQ", "RDBSNG", "ZBPMQFSH", "WLF", "SVFMR"};

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
            //Debug.Log(m.Groups[1].Value + ", " + m.Groups[2].Value + ", " + m.Groups[3].Value);
            data.Add(new Tuple<int, int, int>(int.Parse(m.Groups[1].Value), 
                int.Parse(m.Groups[2].Value), 
                int.Parse(m.Groups[3].Value)));
        }

        Debug.Log(data.Count());
    }


    IEnumerator GetText() {
        UnityWebRequest www = UnityWebRequest.Get("https://raw.githubusercontent.com/mgoadric/AdventOfCode/main/2022/Unity/AdventOfCode2022/Assets/Data/input5.txt");
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            ParseInput(www.downloadHandler.text);

            StartCoroutine("Animate");
        }
    }

    private void ParseInput(string input) {
        List<string> lines = input.Split("\n").ToList();

        data = new List<Tuple<int, int, int>>();
        // https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.match.groups?view=net-7.0
        string pat = @"move (\d+) from (\d+) to (\d+)";
        foreach (string line in lines) {
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);
            Match m = r.Match(line);
            //Debug.Log(m.Groups[1].Value + ", " + m.Groups[2].Value + ", " + m.Groups[3].Value);
            data.Add(new Tuple<int, int, int>(int.Parse(m.Groups[1].Value), 
                int.Parse(m.Groups[2].Value), 
                int.Parse(m.Groups[3].Value)));
        }

        Debug.Log(data.Count());
    }


    private void PartOne() {
        List<Stack<char>> stacks = new List<Stack<char>>();
        foreach (string initial in starting) {
            Stack<char> s = new Stack<char>(initial.ToArray());
            //Debug.Log(s.Peek());
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

    private void PartTwo() {
        List<Stack<char>> stacks = new List<Stack<char>>();
        foreach (string initial in starting) {
            Stack<char> s = new Stack<char>(initial.ToArray());
            //Debug.Log(s.Peek());
            stacks.Add(s);
        }

        foreach (Tuple<int, int, int> instruction in data) {
            Stack<char> temp = new Stack<char>();
            for (int i = 0; i < instruction.Item1; i++) {
                temp.Push(stacks[instruction.Item2 - 1].Pop());
            }
            for (int i = 0; i < instruction.Item1; i++) {
                stacks[instruction.Item3 - 1].Push(temp.Pop());
            }
        }

        string tops = "";
        foreach (Stack<char> s in stacks) {
            tops += s.Peek();
        }
        Debug.Log("Part 2: " + tops);
    }

    IEnumerator Animate() {
        for (int i = 0; i < starting.Length; i++) {
            string initial = starting[i];
            Stack<GameObject> s = new Stack<GameObject>();
            char[] letters = initial.ToArray();
            for (int j = 0; j < letters.Length; j++) {
                GameObject go = Instantiate(cratePrefab, new Vector3(-4 + i, -3 + j, 0), Quaternion.identity);
                go.transform.GetChild(0).GetComponent<TextMeshPro>().text = "" + letters[j];
                s.Push(go);
            }
            //Debug.Log(s.Peek());
            crateStacks.Add(s);
        }

        yield return new WaitForSeconds(1f);

        foreach (Tuple<int, int, int> instruction in data) {
            for (int i = 0; i < instruction.Item1; i++) {
                GameObject top = crateStacks[instruction.Item2 - 1].Pop();
                top.transform.position = new Vector3(-4 + (instruction.Item3 - 1), -3 + crateStacks[instruction.Item3 - 1].Count, 0);
                crateStacks[instruction.Item3 - 1].Push(top);
                yield return new WaitForSeconds(.1f);
            }
        }


    }

    public int TallestStack() {
        return crateStacks.Select(s => s.Count).Max();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //PartOne();

        //PartTwo();

        StartCoroutine("GetText");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
