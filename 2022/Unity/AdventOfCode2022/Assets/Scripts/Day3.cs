using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Day3 : MonoBehaviour
{

    private int priority(char item) {
        string letters = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return letters.IndexOf(item);
    }
    // Start is called before the first frame update
    void Start()
    {

        string path = "Assets/Data/sampleinput3.txt";

        //Read the text from directly from the file
        StreamReader reader = new StreamReader(path); 

        // https://learn.microsoft.com/en-us/dotnet/api/system.string.substring?view=net-7.0
        List<Tuple<string, string>> data = reader.ReadToEnd().Split("\n").Select(
            ruck => new Tuple<string, string>(ruck.Substring(0, ruck.Length / 2),
                   ruck.Substring(ruck.Length / 2))).ToList();

        //Debug.Log(data.Count());


        int sum = 0;
        foreach (Tuple<string, string> ruck in data)
        {
            HashSet<char> comp1 = new HashSet<char>(ruck.Item1);
            HashSet<char> comp2 = new HashSet<char>(ruck.Item2);
            char wrong = comp1.Intersect<char>(comp2).First();
            //Debug.Log(priority(wrong));
            sum += priority(wrong);
        }
        
        Debug.Log(sum);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
