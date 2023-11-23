using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    //to change scene
    public void NextEscena(string escenaName)
    {
        SceneManager.LoadScene(escenaName);
    }
}
