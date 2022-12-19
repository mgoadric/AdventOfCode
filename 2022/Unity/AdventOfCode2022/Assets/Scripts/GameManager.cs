using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// https://medium.com/nerd-for-tech/implementing-a-game-manager-using-the-singleton-pattern-unity-eb614b9b1a74
public class GameManager : MonoBehaviour
{

    private int currentScene = 1;

    private static GameManager _s;

    public static GameManager S {
        get 
        {
            if (_s is null) {
                Debug.LogError("Game Manager is NULL");
            }
            return _s;
        }
    }

    void Awake() {
        _s = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PreviousScene() {
        if (currentScene <= 1) {
            SceneManager.LoadScene("MainMenu");
        } else {
            currentScene--;
            LoadScene();
        }
    }

    public void NextScene() {
        if (currentScene >= 12) {
            SceneManager.LoadScene("MainMenu");
        } else {
            currentScene++;
            LoadScene();
        }
    }

    public void LoadScene() {
        SceneManager.LoadScene("Day" + currentScene);
        if (currentScene != 0) {
            SceneManager.LoadScene("GUI", LoadSceneMode.Additive);
        }
    }
}
