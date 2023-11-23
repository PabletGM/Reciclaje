using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformBasura : MonoBehaviour
{
    //behaviour of the rubbish because by default the prefab was turned
    private void OnEnable()
    {
        this.transform.Rotate(-90, 0, 0);
    }
}
