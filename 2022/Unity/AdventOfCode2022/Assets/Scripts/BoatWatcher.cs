using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatWatcher : MonoBehaviour
{

    public GameObject boat;

    private Day5 crates;

    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        crates = boat.GetComponent<Day5>();
    }

    // Update is called once per frame
    void Update()
    {
        int size = crates.TallestStack();
        camera.orthographicSize = Mathf.Max(5, size / 1.7f);
        transform.position = new Vector3(0, Mathf.Max(5, size / 1.7f) - 5, -10);
    }
}
