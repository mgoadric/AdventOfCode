using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Day1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello");

        string path = "Assets/Data/sampleinput1.txt";

        //Read the text from directly from the input.txt file
        StreamReader reader = new StreamReader(path); 
        List<List<int>> data = reader.ReadToEnd().Split("\n\n").Select(
            elf => elf.Split("\n").Select(s => Int32.Parse(s)).ToList()).ToList();

        Debug.Log(data.Count());
        foreach (List<int> backpack in data) {
            Debug.Log(backpack.Sum());
        }
        reader.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
