using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Day6 : MonoBehaviour
{
    private bool sample = false;

    private List<string> data;

    private int SOP(string signal, int distinct) {
        int[] back = new int[signal.Length];
        for (int i = 0; i < signal.Length; i++) {
            for (int k = 1; k <= distinct; k++) {
                if (i - k >= 0 && signal[i] != signal[i - k]) {
                    back[i]++;
                } else {
                    break;
                }
            }
            //Debug.Log(back[i]);
            bool good = true;
            for (int k = 0; k < distinct; k++) {
                if (i - k < 0 || back[i - k] < distinct - k) {
                    good = false;
                    break;
                }
            }
            if (good) {
                return i + 1;
            }
        }
        return -1;
    }

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
        LoadData(path + "input6.txt");
        
        foreach (string signal in data) {
            int distinct = 14;
            int start = SOP(signal, distinct - 1);
            Debug.Log(start + ": " + signal.Substring(start - distinct, distinct));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
