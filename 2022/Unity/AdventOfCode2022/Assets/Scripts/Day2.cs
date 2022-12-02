using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Day2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello");

        string path = "Assets/Data/sampleinput2.txt";

        //Read the text from directly from the file
        StreamReader reader = new StreamReader(path); 
        List<List<string>> data = reader.ReadToEnd().Split("\n").Select(
            elf => elf.Split(" ").ToList()).ToList();

        Debug.Log(data.Count());
        foreach (List<string> game in data) {
            //Debug.Log(game[0] + " -> " + game[1]);
        }
        reader.Close();

        // https://stackoverflow.com/questions/1273139/c-sharp-java-hashmap-equivalent
        Dictionary<string, string> moveMap = new Dictionary<string, string>();
        moveMap["X"] = "A";
        moveMap["Y"] = "B";
        moveMap["Z"] = "C";

/*
        Dictionary<Tuple<string, string>, int> gameScores = new Dictionary<Tuple<string, string>, int>();
        // https://stackoverflow.com/questions/723211/quick-way-to-create-a-list-of-values-in-c
        gameScores[new Tuple<string, string>("A", "X")] = 3;
        gameScores[new Tuple<string, string>("B", "Y")] = 3;
        gameScores[new Tuple<string, string>("C", "Z")] = 3;
        gameScores[new Tuple<string, string>("A", "Y")] = 6;
        gameScores[new Tuple<string, string>("B", "Z")] = 6;
        gameScores[new Tuple<string, string>("C", "X")] = 6;
        gameScores[new Tuple<string, string>("A", "Z")] = 0;
        gameScores[new Tuple<string, string>("B", "X")] = 0;
        gameScores[new Tuple<string, string>("C", "Y")] = 0;

*/
        Dictionary<string, int> moveScores = new Dictionary<string, int>();
        moveScores["A"] = 1;
        moveScores["B"] = 2;
        moveScores["C"] = 3;

        foreach (List<string> game in data) {
            //Debug.Log(moveScores[game[1]]);
        }

        Dictionary<Tuple<string, string>, int> gameScores = new Dictionary<Tuple<string, string>, int>();
        // https://stackoverflow.com/questions/723211/quick-way-to-create-a-list-of-values-in-c
        gameScores[new Tuple<string, string>("A", "A")] = 3;
        gameScores[new Tuple<string, string>("B", "B")] = 3;
        gameScores[new Tuple<string, string>("C", "C")] = 3;
        gameScores[new Tuple<string, string>("A", "B")] = 6;
        gameScores[new Tuple<string, string>("B", "C")] = 6;
        gameScores[new Tuple<string, string>("C", "A")] = 6;
        gameScores[new Tuple<string, string>("A", "C")] = 0;
        gameScores[new Tuple<string, string>("B", "A")] = 0;
        gameScores[new Tuple<string, string>("C", "B")] = 0;

        foreach (List<string> game in data) {
            //Debug.Log(gameScores[new Tuple<string, string>(game[0], game[1])]);
        }

        int sum = data.Select(game => moveScores[moveMap[game[1]]] + gameScores[new Tuple<string, string>(game[0], moveMap[game[1]])]).Sum();
        Debug.Log("Part 1 Total = " + sum);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
