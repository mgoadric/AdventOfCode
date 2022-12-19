using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Day1 : MonoBehaviour
{

    private bool sample = false;

    private List<List<int>> data;

    public GameObject elfPrefab;
    public GameObject backpackPrefab;

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
        float x = -6;
        float y = -3.5f;
        foreach(List<int> backpack in data) 
        {
            float startX = UnityEngine.Random.Range(-6, 6);

            // Add Elf
            GameObject go = Instantiate(elfPrefab, new Vector3(startX, 6, 0), Quaternion.identity);

            // Add Backpack
            GameObject bp = Instantiate(backpackPrefab, new Vector3(startX, 6, 1), Quaternion.identity);
            bp.gameObject.transform.parent = go.transform;
            bp.GetComponent<SpriteRenderer>().size = new Vector2(1, backpack.Sum() / 60000.0f);

            go.gameObject.transform.localScale = new Vector3(0.25f, 0.25f, 1);

            // Start Movement
            go.GetComponent<ElfMovement>().target = new Vector3(x, y, 0);

            // Wait 0.1 second before the next 
            yield return new WaitForSeconds(.1f);
            x += 0.5f;
            if (x >= 6) {
                x = -6;
                y += 0.75f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
