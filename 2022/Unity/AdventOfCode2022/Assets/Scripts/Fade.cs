using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{

    public Color on = new Color(1, 0.8f, 0);
    // Start is called before the first frame update
    void Start()
    {
        TurnOn();
    }

    IEnumerator FadePixel()
    {
        Color c = GetComponent<Renderer>().material.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(.1f);        
        }
    }

    public void TurnOn() {
        StopAllCoroutines();
        GetComponent<Renderer>().material.color = new Color(on.r, on.g, on.b, on.a);
        StartCoroutine("FadePixel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
