using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour
{

    public GameObject dayLabel;
    // Start is called before the first frame update
    void Start()
    {
        TMPro.TextMeshProUGUI dayText = dayLabel.GetComponent<TMPro.TextMeshProUGUI>();
        dayText.text = "Day " + GameManager.S.CurrentScene;
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
