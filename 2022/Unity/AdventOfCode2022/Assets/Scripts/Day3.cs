using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Day3 : MonoBehaviour
{

    private string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private int priority(char item) {
        return letters.IndexOf(item) + 1;
    }
    // Start is called before the first frame update
    void Start()
    {

        string path = "Assets/Data/input3.txt";

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
            comp1.IntersectWith(comp2);
            char wrong = comp1.First();
            //Debug.Log(priority(wrong));
            sum += priority(wrong);
        }
        
        Debug.Log("Part 1: " + sum);

        int sum2 = 0;
        for (int i = 0; i < data.Count(); i += 3) {
            HashSet<char> all = new HashSet<char>(letters);
            for (int k = i; k < i + 3; k++) {
                HashSet<char> comp1 = new HashSet<char>(data[k].Item1);
                HashSet<char> comp2 = new HashSet<char>(data[k].Item2);
                comp1.UnionWith(comp2);
                all.IntersectWith(comp1);
            }
            char badge = all.First();
            //Debug.Log(priority(wrong));
            sum2 += priority(badge);
        }

        Debug.Log("Part 2: " + sum2);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
