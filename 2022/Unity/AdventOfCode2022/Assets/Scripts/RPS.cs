using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPS : MonoBehaviour
{

    public Sprite rock;
    public Sprite paper;
    public Sprite scissors;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void MakeRock() {
        spriteRenderer.sprite = rock;
    }
    public void MakePaper() {
        spriteRenderer.sprite = paper;
    }
    public void MakeScissors() {
        spriteRenderer.sprite = scissors;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
