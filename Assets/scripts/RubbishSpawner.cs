using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbishSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //aparece basura
        GameController.instance.AparecerBasura();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
