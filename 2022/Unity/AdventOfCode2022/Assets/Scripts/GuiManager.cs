using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PreviousScene() {
        GameManager.S.PreviousScene();
    }

    public void NextScene() {
        GameManager.S.NextScene();
    }
}
