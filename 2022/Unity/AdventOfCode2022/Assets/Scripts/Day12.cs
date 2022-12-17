using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Day12 : MonoBehaviour
{

    public GameObject terrain;
    public GameObject start;

    public GameObject end;

    private float[,] worldmap;

    private void LoadData(string filename) {

        //Read the text from directly from the input.txt file
        StreamReader reader = new StreamReader(filename); 
        List<string> data = reader.ReadToEnd().Split("\n").ToList();
        reader.Close();

        Debug.Log(data.Count());

        worldmap = new float[data.Count, data[0].Length];
        for (int i = 0; i < data.Count; i++) {
            char[] letters = data[i].ToCharArray();
            for (int j = 0; j < letters.Length; j++) {
                if (letters[j] == 'S') {
                    // Make start here
                    start.transform.position = new Vector3(100 + i, 0, 100 + j);
                    letters[j] = 'a';
                } else if (letters[j] == 'E') {
                    // Make end here
                    end.transform.position = new Vector3(100 + i, 0, 100 + j);
                    letters[j] = 'z';
                }
                worldmap[i, j] = (2 + (letters[j] - 'a')) / 280.0f;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        string path = "Assets/Data/input12.txt";
        LoadData(path);
        Terrain ter = terrain.GetComponent<Terrain>();
        ter.terrainData.SetHeights(100, 100, worldmap);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
