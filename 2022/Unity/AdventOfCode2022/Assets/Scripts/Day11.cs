using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day11 : MonoBehaviour
{

    public List<Monkey> monkeys = new List<Monkey>();
    public GameObject[] monkeySprites = new GameObject[8];

    private List<Queue<GameObject>> crateSprites = new List<Queue<GameObject>>();

    public GameObject cratePrefab;

    // Start is called before the first frame update
    void Start()
    {
        /*
        SAMPLE INPUT

        Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

    Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1

        Monkey m;
        m = new Monkey('*', 19, false, 23, 2, 3);
        m.Add(79);
        m.Add(98);
        monkeys.Add(m);
        m = new Monkey('+', 6, false, 19, 2, 0);
        m.Add(54);
        m.Add(65);
        m.Add(75);
        m.Add(74);
        monkeys.Add(m);
        m = new Monkey('#', 0, true, 13, 1, 3);
        m.Add(79);
        m.Add(60);
        m.Add(97);
        monkeys.Add(m);
        m = new Monkey('+', 3, false, 17, 0, 1);
        m.Add(74);
        monkeys.Add(m);
        */



        /*
        FULL INPUT


    */

/*
Monkey 0:
  Starting items: 74, 73, 57, 77, 74
  Operation: new = old * 11
  Test: divisible by 19
    If true: throw to monkey 6
    If false: throw to monkey 7
*/
        Monkey m;
        m = new Monkey('*', 11, false, 19, 6, 7);
        m.Add(74);
        m.Add(73);
        m.Add(57);
        m.Add(77);
        m.Add(74);
        monkeys.Add(m);

/*
Monkey 1:
  Starting items: 99, 77, 79
  Operation: new = old + 8
  Test: divisible by 2
    If true: throw to monkey 6
    If false: throw to monkey 0
*/
        m = new Monkey('+', 8, false, 2, 6, 0);
        m.Add(99);
        m.Add(77);
        m.Add(79);
        monkeys.Add(m);

/*
Monkey 2:
  Starting items: 64, 67, 50, 96, 89, 82, 82
  Operation: new = old + 1
  Test: divisible by 3
    If true: throw to monkey 5
    If false: throw to monkey 3
*/
        m = new Monkey('+', 1, false, 3, 5, 3);
        m.Add(64);
        m.Add(67);
        m.Add(50);        
        m.Add(96);
        m.Add(89);
        m.Add(82);
        m.Add(82);
        monkeys.Add(m);

/*
Monkey 3:
  Starting items: 88
  Operation: new = old * 7
  Test: divisible by 17
    If true: throw to monkey 5
    If false: throw to monkey 4
*/
        m = new Monkey('*', 7, false, 17, 5, 4);
        m.Add(88);
        monkeys.Add(m);

/*
Monkey 4:
  Starting items: 80, 66, 98, 83, 70, 63, 57, 66
  Operation: new = old + 4
  Test: divisible by 13
    If true: throw to monkey 0
    If false: throw to monkey 1
*/
        m = new Monkey('+', 4, false, 13, 0, 1);
        m.Add(80);
        m.Add(66);
        m.Add(98);
        m.Add(83);
        m.Add(70);
        m.Add(63);
        m.Add(57);
        m.Add(66);
        monkeys.Add(m);

/*
Monkey 5:
  Starting items: 81, 93, 90, 61, 62, 64
  Operation: new = old + 7
  Test: divisible by 7
    If true: throw to monkey 1
    If false: throw to monkey 4
*/
        m = new Monkey('+', 7, false, 7, 1, 4);
        m.Add(81);
        m.Add(93);
        m.Add(90);
        m.Add(61);
        m.Add(62);
        m.Add(64);
        monkeys.Add(m);

/*
Monkey 6:
  Starting items: 69, 97, 88, 93
  Operation: new = old * old
  Test: divisible by 5
    If true: throw to monkey 7
    If false: throw to monkey 2
*/
        m = new Monkey('#', 0, true, 5, 7, 2);
        m.Add(69);
        m.Add(97);
        m.Add(88);
        m.Add(93);
        monkeys.Add(m);

/*
Monkey 7:
  Starting items: 59, 80
  Operation: new = old + 6
  Test: divisible by 11
    If true: throw to monkey 2
    If false: throw to monkey 3
*/
        m = new Monkey('+', 6, false, 11, 2, 3);
        m.Add(59);
        m.Add(80);
        monkeys.Add(m);

        for (int i = 0; i < monkeys.Count; i++) {
            Monkey mon = monkeys[i];
            crateSprites.Add(new Queue<GameObject>());
            for (int j = 0; j < mon.NumItems(); j++) {
                GameObject go = Instantiate(cratePrefab, 
                    monkeySprites[i].transform.position + new Vector3(0, j * 0.2f, 0), 
                    Quaternion.identity);
                go.GetComponent<Thrown>().target = go.transform.position;
                crateSprites[i].Enqueue(go);
            }
        }

        StartCoroutine("ThrowThings");
    }

    IEnumerator ThrowThings() {
        yield return new WaitForSeconds(1f);
        while (true) {
            //print("Round " + rounds);
            for (int i = 0; i < monkeys.Count; i++) {
                Monkey mon = monkeys[i];
                // print("Monkey " + i + ":");
                while (mon.HasItems()) {
                    Tuple<long, int> result = mon.Inspect();
                    monkeys[result.Item2].Add(result.Item1);
                    GameObject c = crateSprites[i].Dequeue();
                    c.GetComponent<Thrown>().target = monkeySprites[result.Item2].transform.position + new Vector3(0, crateSprites[result.Item2].Count * 0.2f, 0);
                    crateSprites[result.Item2].Enqueue(c);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            for (int i = 0; i < monkeys.Count; i++) {
                Monkey mon = monkeys[i];
                //print("Monkey " + i + " has " + mon.MyItems());
            }
            for (int i = 0; i < monkeys.Count; i++) {
                Monkey mon = monkeys[i];
                //print("Monkey " + i + " inspected " + mon.Inspections());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
