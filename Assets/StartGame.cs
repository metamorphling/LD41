using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    void Start()
    {
        Screen.SetResolution(1024, 576, false);
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene("Level1");
    }
}
