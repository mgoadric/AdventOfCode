using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

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

        worldmap = new float[data.Count * 2, data[0].Length * 2];
        for (int i = 0; i < data.Count; i++) {
            char[] letters = data[i].ToCharArray();
            for (int j = 0; j < letters.Length; j++) {
                if (letters[j] == 'S') {
                    // Make start here
                    //start.transform.position = new Vector3(4 * j, 4, 4 * i);
                    print("Start at " + j + ", " + i);
                    letters[j] = (char)('a' - 1);
                } else if (letters[j] == 'E') {
                    // Make end here
                    //end.transform.position = new Vector3(4 * j, 60, 4 * i);
                    print("End at " + j + ", " + i);

                    letters[j] = (char)('z' + 1);
                }
                worldmap[((data.Count - 1 - i) * 2), j * 2] = (2 + (letters[j] - 'a')) / (28 * 10.0f);
                worldmap[((data.Count - 1 - i) * 2), j * 2 + 1] = (2 + (letters[j] - 'a')) / (28 * 10.0f);
                worldmap[((data.Count - 1 - i) * 2) + 1, j * 2] = (2 + (letters[j] - 'a')) / (28 * 10.0f);
                worldmap[((data.Count - 1 - i) * 2) + 1, j * 2 + 1] = (2 + (letters[j] - 'a')) / (28 * 10.0f);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        string path = "Assets/Data/input12.txt";
        LoadData(path);
        Terrain ter = terrain.GetComponent<Terrain>();
        ter.terrainData.SetHeights(0, 0, worldmap);
        */

        NavMeshAgent agent = start.GetComponent<NavMeshAgent>();
        agent.destination = end.transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
