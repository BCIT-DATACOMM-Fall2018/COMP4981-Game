using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour {


    public void loadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
