using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Day2 : MonoBehaviour
{

    public GameObject elfPrefab;

    public GameObject elfRPS;

    public GameObject myRPS;

    public GameObject me;

    private List<List<string>> data;

    private Dictionary<string, string> moveMap = new Dictionary<string, string>();

    private Dictionary<Tuple<string, string>, string> moveLookup = new Dictionary<Tuple<string, string>, string>();

    private Dictionary<string, int> moveScores = new Dictionary<string, int>();

    private Dictionary<Tuple<string, string>, int> gameScores = new Dictionary<Tuple<string, string>, int>();

    private Queue<ElfMovement> elves;

    IEnumerator GetText() {
        UnityWebRequest www = UnityWebRequest.Get("https://raw.githubusercontent.com/mgoadric/AdventOfCode/main/2022/Unity/AdventOfCode2022/Assets/Data/input2.txt");
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            ParseInput(www.downloadHandler.text);
            Debug.Log(data.Count());

            int startX = 0;
            foreach (List<string> game in data) {
                //Debug.Log(gameScores[new Tuple<string, string>(game[0], game[1])]);
                GameObject go = Instantiate(elfPrefab, new Vector3(startX, 1, 0), Quaternion.identity);
                ElfMovement move = go.GetComponent<ElfMovement>();
                elves.Enqueue(move);
                move.target = new Vector3(startX, 1, 0);
                startX--;
            }

            int sum = data.Select(game => moveScores[moveMap[game[1]]] + gameScores[new Tuple<string, string>(game[0], moveMap[game[1]])]).Sum();
            Debug.Log("Part 1 Total = " + sum);

            int sum2 = data.Select(game => moveScores[moveLookup[new Tuple<string, string>(game[0], game[1])]] + 
            gameScores[new Tuple<string, string>(game[0], moveLookup[new Tuple<string, string>(game[0], game[1])])]).Sum();
            Debug.Log("Part 2 Total = " + sum2);

            StartCoroutine("PlayGames");
        }
    }

    private void ParseInput(string input) {
        data = input.Split("\n").Select(
            elf => elf.Split(" ").ToList()).ToList();

    }

    // Start is called before the first frame update
    void Start()
    {

        elves = new Queue<ElfMovement>();
        //elfRPS.SetActive(false);
        //myRPS.SetActive(false);

        // https://stackoverflow.com/questions/1273139/c-sharp-java-hashmap-equivalent
        moveMap["X"] = "A";
        moveMap["Y"] = "B";
        moveMap["Z"] = "C";

        moveLookup[new Tuple<string, string>("A", "X")] = "C";
        moveLookup[new Tuple<string, string>("B", "Y")] = "B";
        moveLookup[new Tuple<string, string>("C", "Z")] = "A";
        moveLookup[new Tuple<string, string>("A", "Y")] = "A";
        moveLookup[new Tuple<string, string>("B", "Z")] = "C";
        moveLookup[new Tuple<string, string>("C", "X")] = "B";
        moveLookup[new Tuple<string, string>("A", "Z")] = "B";
        moveLookup[new Tuple<string, string>("B", "X")] = "A";
        moveLookup[new Tuple<string, string>("C", "Y")] = "C";

        moveScores["A"] = 1;
        moveScores["B"] = 2;
        moveScores["C"] = 3;

        // https://stackoverflow.com/questions/723211/quick-way-to-create-a-list-of-values-in-c
        gameScores[new Tuple<string, string>("A", "A")] = 3;
        gameScores[new Tuple<string, string>("B", "B")] = 3;
        gameScores[new Tuple<string, string>("C", "C")] = 3;
        gameScores[new Tuple<string, string>("A", "B")] = 6;
        gameScores[new Tuple<string, string>("B", "C")] = 6;
        gameScores[new Tuple<string, string>("C", "A")] = 6;
        gameScores[new Tuple<string, string>("A", "C")] = 0;
        gameScores[new Tuple<string, string>("B", "A")] = 0;
        gameScores[new Tuple<string, string>("C", "B")] = 0;

        StartCoroutine("GetText");

    }

    void ShowRPS(string what, GameObject rps) {  
        if (what == "A") {
            rps.GetComponent<RPS>().MakeRock();
        } else if (what == "B") {
            rps.GetComponent<RPS>().MakePaper();
        } else {
            rps.GetComponent<RPS>().MakeScissors();
        }
    }

    IEnumerator PlayGames() {
        yield return new WaitForSeconds(1f);
        foreach (List<string> game in data) {
            //Debug.Log(gameScores[new Tuple<string, string>(game[0], game[1])]);

            print("Setting up prefabs");
            ShowRPS(game[0], elfRPS);
            ShowRPS(moveLookup[new Tuple<string, string>(game[0], game[1])], myRPS);

            print(moveLookup[new Tuple<string, string>(game[0], game[1])]);
            elfRPS.SetActive(true);
            myRPS.SetActive(true);
            yield return new WaitForSeconds(1f);

            elfRPS.SetActive(false);
            myRPS.SetActive(false);

            ElfMovement mymove = me.GetComponent<ElfMovement>();
            Vector3 mycurrent = mymove.gameObject.transform.position;
            mymove.target = new Vector3(mycurrent.x - 1, mycurrent.y, mycurrent.z);


            ElfMovement move = elves.Dequeue();
            Vector3 current = move.gameObject.transform.position;
            move.target = new Vector3(current.x, current.y + 8, current.z);
            yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
