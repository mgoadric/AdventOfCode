using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSprite : MonoBehaviour
{

    public Sprite start;

    public Sprite end;

    public GameObject ropePrefab;

    public GameObject knotPrefab;

    private Rope rope;

    private GameObject[] knots;

    private GameObject[] ropes;

    public int size = 10;


    // Start is called before the first frame update
    void Start()
    {
        rope = new Rope(0, 0, size);
        knots = new GameObject[size];
        ropes = new GameObject[size - 1];

        for(int i = 0; i < rope.knots.Length; i++) {
            knots[i] = Instantiate(knotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            
            if (i == 0) {
                knots[i].GetComponent<SpriteRenderer>().sprite = start;
            } else if (i == rope.knots.Length - 1) {
                knots[i].GetComponent<SpriteRenderer>().sprite = end;
            }

            float angle = 360 * UnityEngine.Random.Range(0, 1);
            knots[i].transform.eulerAngles = new Vector3(0, 0, angle);

            if (i >= 1) {
                ropes[i - 1] = Instantiate(ropePrefab, new Vector3(0, 0, 1), Quaternion.identity);
                ropes[i - 1].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            print("up");
            rope.Move(new Tuple<char, int>('U', 1));
        } else if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))
        {
            print("up");
            rope.Move(new Tuple<char, int>('D', 1));
        } else if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            print("up");
            rope.Move(new Tuple<char, int>('L', 1));
        } else if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))
        {
            print("up");
            rope.Move(new Tuple<char, int>('R', 1));
        }


        for(int i = 0; i < rope.knots.Length; i++) {
            knots[i].transform.position = new Vector3(rope.knots[i].x, rope.knots[i].y, 0);
            if (i >= 1) {
                if (rope.knots[i].Distance(rope.knots[i - 1]) > 0.5) {
                    ropes[i - 1].transform.position = new Vector3(
                        (rope.knots[i].x + rope.knots[i-1].x) / 2.0f,
                        (rope.knots[i].y + rope.knots[i-1].y) / 2.0f,
                        1);
                    float angle = Mathf.Rad2Deg * (Mathf.Atan2(rope.knots[i].y - rope.knots[i-1].y, rope.knots[i].x - rope.knots[i-1].x));
                    ropes[i - 1].transform.eulerAngles = new Vector3(0, 0, angle);
                    ropes[i - 1].SetActive(true);
                } else {
                    ropes[i - 1].SetActive(false);
                }
            }
        }
    }
}
