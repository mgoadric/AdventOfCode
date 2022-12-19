using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Day1 : MonoBehaviour
{

    private bool sample = false;

    private List<List<int>> data;

    public GameObject elfPrefab;
    public GameObject backpackPrefab;

    IEnumerator GetText() {
        UnityWebRequest www = UnityWebRequest.Get("https://raw.githubusercontent.com/mgoadric/AdventOfCode/main/2022/Unity/AdventOfCode2022/Assets/Data/input1.txt");
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            ParseInput(www.downloadHandler.text);
 
            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;

            foreach (List<int> backpack in data) {
                Debug.Log(backpack.Sum());
            }

            StartCoroutine("SpawnElf");
        }
    }

    private void LoadData(string filename) {

        //Read the text from directly from the input.txt file
        StreamReader reader = new StreamReader(filename); 
        ParseInput(reader.ReadToEnd());
        reader.Close();

        Debug.Log(data.Count());
    }

    private void ParseInput(string input) {
        data = input.Split("\n\n").Select(
            line => line.Split("\n").Select(s => Int32.Parse(s)).ToList()).ToList();
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        string path = "Assets/Data/";
        if (sample) {
            path += "sample";
        }
        LoadData(path + "input1.txt");
        
        foreach (List<int> backpack in data) {
            Debug.Log(backpack.Sum());
        }
        
        StartCoroutine("SpawnElf");
        */
        StartCoroutine("GetText");

    }

    IEnumerator SpawnElf()
    {
        float x = -6.5f;
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
            if (x >= 6.5f) {
                x = -6.5f;
                y += 0.75f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
