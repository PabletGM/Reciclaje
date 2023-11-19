using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformBasura : MonoBehaviour
{
    private void OnEnable()
    {
        this.transform.Rotate(-90, 0, 0);
    }
}
