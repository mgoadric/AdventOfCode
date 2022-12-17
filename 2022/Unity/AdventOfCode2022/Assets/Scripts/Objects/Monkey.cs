using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monkey {
    
    private Queue<long> items = new Queue<long>();
    private char operation;
    private int operand;
    private bool dup;
    private int divisible;
    private Tuple<int, int> destinations;

    private int inspectCount;

    public Monkey(char operation, int operand, bool dup, int divisible, int d1, int d2) {
        this.operation = operation;
        this.operand = operand;
        this.dup = dup;
        this.divisible = divisible;
        destinations = new Tuple<int, int>(d1, d2);
    }

    public void Add(long item) {
        items.Enqueue(item);
    }

    public bool HasItems() {
        return items.Count() > 0;
    }

    public String MyItems() {
        string s = "[";
        foreach (long item in items) {
            s += item + ", ";
        }
        s += "]";
        return s;
    }

    public int Inspections() {
        return inspectCount;
    }

    public Tuple<long, int> Inspect() {
        inspectCount++;

        long item = items.Dequeue();
        // Debug.Log("  Monkey inspects an item with a worry level of " + item);

        if (dup) {
            long prev = item;
            item *= item;
            // Debug.Log("    Worry level is doubled to " + item);
        } else if (operation == '*') {
            item *= operand;
            // Debug.Log("    Worry level is multiplied by " + operand + " to " + item);
        } else {
            item += operand;
            // Debug.Log("    Worry level is increased by " + operand + " to " + item);
        }

        //item /= 3;
        // Debug.Log("    Monkey gets bored with item. Worry level is divided by 3 to " + item);

        if (item % divisible == 0) {
            // Debug.Log("    Current worry level is divisible by " + divisible);
            // Debug.Log("    Item with worry level " + item + " is thrown to monkey " + destinations.Item1);

            return new Tuple<long, int>(item, destinations.Item1);
        } else {
            // Debug.Log("    Current worry level is not divisible by " + divisible);
            // Debug.Log("    Item with worry level " + item + " is thrown to monkey " + destinations.Item2);

            return new Tuple<long, int>(item, destinations.Item2);
        }
    }
}