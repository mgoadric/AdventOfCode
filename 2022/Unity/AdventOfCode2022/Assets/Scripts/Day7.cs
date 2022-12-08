using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Day7 : MonoBehaviour
{
    private bool sample = false;

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
        LoadData(path + "input7.txt");
        
        FileDir root = new FileDir(0, null);
        FileDir current = root;

        foreach (string line in data) {
            if (line.StartsWith("$ ls")) {

            } else if (line.StartsWith("$ cd")) {
                string where = line.Substring(5);
                print("changing into dir " + where);
                switch (where) {
                    case "/":
                        current = root;
                        break;
                    case "..":
                        current = current.parent;
                        break;
                    default:
                        current = current.files[where];
                        break;
                }
            } else if (line.StartsWith("dir")) {
                print("adding subdirectory " + line.Substring(4));
                current.files.Add(line.Substring(4), new FileDir(0, current));
            } else {
                print("adding file " + line);
                string[] pieces = line.Split(" ");
                current.files.Add(pieces[1], new FileDir(int.Parse(pieces[0]), current));
            }
        }

        int rootSize = root.FileSize();
        print(rootSize);

        List<int> sizes = new List<int>();
        root.AllFileSize(sizes);
        foreach(int i in sizes) {
            print("dirsize " + i);
        }

        print("Part 1: " + sizes.Where(x => x <= 100000).Sum());

        int free = 70000000 - rootSize;

        print("Part 2: " + sizes.Where(x => free + x > 30000000).OrderByDescending(x => x).Last());

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
