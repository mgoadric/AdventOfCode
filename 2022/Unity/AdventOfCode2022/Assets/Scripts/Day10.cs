using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Day10 : MonoBehaviour
{

    private bool sample = true;

    private List<string> data;

    private void LoadData(string filename) {

        //Read the text from directly from the input.txt file
        StreamReader reader = new StreamReader(filename); 
        data = reader.ReadToEnd().Split("\n").ToList();
        reader.Close();

        Debug.Log(data.Count());
    }

    // Start is called before the first frame update
    void Start()
    {
        string path = "Assets/Data/";
        if (sample) {
            path += "sample";
        }
        LoadData(path + "input10.txt");

        int signalStrengths = 0;
        int clock = 1;
        int x = 1;
        foreach (string instruction in data) {
            string command = instruction.Substring(0, 4);
            int v = 0;
            int ticks = 1;
            switch(command) {
                case "addx":
                    v = int.Parse(instruction.Substring(5));
                    ticks++;
                    break;
            }
            while (ticks > 0) {
                if ((clock + 20) % 40 == 0) {
                    print(x + ", " + clock + " = " + clock * x);
                    signalStrengths += clock * x;
                }
                ticks--;
                clock++;
            }
            x += v;
        }
        print("Part 1: " + signalStrengths);
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
