using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Day8 : MonoBehaviour
{

    private bool sample = false;

    private List<List<int>> data;

    private void LoadData(string filename) {

        //Read the text from directly from the input.txt file
        StreamReader reader = new StreamReader(filename); 
        List<string> file = reader.ReadToEnd().Split("\n").ToList();
        reader.Close();

        Debug.Log(file.Count());

        data = new List<List<int>>();
        foreach(string line in file) {
            data.Add(line.ToList().Select(x => x - 48).ToList());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        string path = "Assets/Data/";
        if (sample) {
            path += "sample";
        }
        LoadData(path + "input8.txt");

        for (int i = 0; i < data.Count(); i++) {
            for (int j = 0; j < data[i].Count(); j++) {
                //print(data[i][j]);
            }
        }

        bool[,] visible = new bool[data.Count(),data.Count()];

        for (int i = 0; i < data.Count(); i++) {
            int height = data[i][0];
            //print("Start height " + height);
            visible[i, 0] = true;
            for (int j = 1; j < data[i].Count(); j++) {
                if (data[i][j] > height) {
                    visible[i,j] = true;
                    height = data[i][j];
                    //print("new height " + height);
                }
            }
        }

        for (int i = 0; i < data.Count(); i++) {
            int height = data[i][data.Count() - 1];
            //print("Start height " + height);
            visible[i, data.Count() - 1] = true;
            for (int j = data.Count() - 2; j >= 0; j--) {
                if (data[i][j] > height) {
                    visible[i,j] = true;
                    height = data[i][j];
                    //print("new height " + height);
                }
            }
        }

        for (int i = 0; i < data.Count(); i++) {
            int height = data[0][i];
            //print("Start height " + height);
            visible[0, i] = true;
            for (int j = 1; j < data[i].Count(); j++) {
                if (data[j][i] > height) {
                    visible[j,i] = true;
                    height = data[j][i];
                    //print("new height " + height);
                }
            }
        }

        for (int i = 0; i < data.Count(); i++) {
            int height = data[data.Count() - 1][i];
            //print("Start height " + height);
            visible[data.Count() - 1, i] = true;
            for (int j = data.Count() - 2; j >= 0; j--) {
                if (data[j][i] > height) {
                    visible[j,i] = true;
                    height = data[j][i];
                    //print("new height " + height);
                }
            }
        }

        int visibleCount = 0;
        for (int i = 0; i < data.Count(); i++) {
            for (int j = 0; j < data[i].Count(); j++) {
                if (visible[i,j]) {
                    visibleCount++;
                }
            }
        }

        print("Part 1: " + visibleCount);

        Tuple<int, int>[] dirs = {
            new Tuple<int, int>(0, 1),
            new Tuple<int, int>(1, 0),
            new Tuple<int, int>(0, -1),
            new Tuple<int, int>(-1, 0),
        };

        int bestScenic = 0;
        for (int i = 1; i < data.Count() - 1; i++) {
            for (int j = 1; j < data[i].Count() - 1; j++) {
                
                int scenicScore = 1;
                foreach(Tuple<int, int> d in dirs) {
                    int x = i;
                    int y = j;
                    int distance = 1;
                    bool looking = true;
                    do {
                        x += d.Item1;
                        y += d.Item2;
                        //print("comparing " + data[x][y] + " to " + data[i][j]);
                        if (x == 0 || x == data.Count() - 1 || y == 0 || y == data.Count() - 1) {
                            //print("edge, stopping");
                            looking = false;
                        } else if (data[x][y] < data[i][j]) {
                            //print("less");
                            distance++;
                        } else if (data[x][y] >= data[i][j]) {
                            //print("stopping");
                            looking = false;
                        }
                    } while (looking);
                    scenicScore *= distance;
                }
                if (scenicScore > bestScenic) {
                    bestScenic = scenicScore;
                    //print("best yet at " + i + ", " + j + ": " + bestScenic);
                }
            }
        }

        print("Part 2:" + bestScenic);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
