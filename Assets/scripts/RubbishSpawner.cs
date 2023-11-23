using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbishSpawner : MonoBehaviour
{
    //gives the order to put each spot with a rubbish prefab
    void Start()
    {
        //aparece basura
        GameController.instance.AparecerBasura();
    }

}
