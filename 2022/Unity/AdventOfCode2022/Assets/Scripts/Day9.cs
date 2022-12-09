using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Day9 : MonoBehaviour
{

    private bool sample = false;

    private List<Tuple<char, int>> data;

    private void LoadData(string filename) {

        //Read the text from directly from the input.txt file
        StreamReader reader = new StreamReader(filename); 
        data = reader.ReadToEnd().Split("\n").ToList().Select(motion => new Tuple<char, int>(motion[0], int.Parse(motion.Substring(2)))).ToList();
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
        LoadData(path + "input9.txt"); 

        Rope r = new Rope(0, 0);
        foreach (Tuple<char, int> movement in data) {
            r.Move(movement);
        }
        print("Part 1: " + r.TailSpaces());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
