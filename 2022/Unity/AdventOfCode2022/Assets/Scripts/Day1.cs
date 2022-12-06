using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Day1 : MonoBehaviour
{

    private bool sample = true;

    private List<List<int>> data;

    public GameObject elfPrefab;

    private void LoadData(string filename) {

        //Read the text from directly from the input.txt file
        StreamReader reader = new StreamReader(filename); 
        data = reader.ReadToEnd().Split("\n\n").Select(
            line => line.Split("\n").Select(s => Int32.Parse(s)).ToList()).ToList();
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
        LoadData(path + "input1.txt");
        
        foreach (List<int> backpack in data) {
            Debug.Log(backpack.Sum());
        }
        
        StartCoroutine("SpawnElf");

    }

    IEnumerator SpawnElf()
    {
        int x = -4;
        foreach(List<int> backpack in data) 
        {
            GameObject go = Instantiate(elfPrefab, new Vector3(0, 8, 0), Quaternion.identity);
            go.GetComponent<ElfMovement>().target = new Vector3(x, 0, 0);
            yield return new WaitForSeconds(1f);
            x += 2;
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    
}
