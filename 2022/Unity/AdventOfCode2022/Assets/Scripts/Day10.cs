using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Day10 : MonoBehaviour
{

    public GameObject pixelPrefab;

    public GameObject[,] screen = new GameObject[40, 6];

    [Range(1.0f, 10.0f)]
    public float raySpeed = 1;

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
        LoadData(path + "input10.txt");

        for(int i = 0; i < 40; i++) {
            for (int j = 0; j < 6; j++) {
                screen[i,j] = Instantiate(pixelPrefab, new Vector3(0.3f * (i - 20), 0.3f * (3 - j), 0), Quaternion.identity);
                screen[i,j].GetComponent<Fade>().TurnOn();
            }
        }

        StartCoroutine("DrawScreen");

    }

    IEnumerator DrawScreen() {
        while(true) {
            int signalStrengths = 0;
            int clock = 1;
            int x = 1;
            int y = 1;
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
                        //print(x + ", " + clock + " = " + clock * x);
                        signalStrengths += clock * x;
                    }

                    int sx = (clock - 1) % 40;
                    if (sx >= x - 1 && sx <= x + 1) {
                        //print("firing " + (sx) + ", " + y);
                        screen[sx, y - 1].GetComponent<Fade>().TurnOn();
                    }

                    if (clock % 40 == 0) {
                        y++;
                    }
                    ticks--;
                    clock++;
                    yield return new WaitForSeconds(.1f / Mathf.Pow(2, raySpeed));
                }
                x += v;
            }
            //print("Part 1: " + signalStrengths);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
