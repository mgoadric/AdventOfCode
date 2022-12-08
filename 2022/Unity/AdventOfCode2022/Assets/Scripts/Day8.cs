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


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
