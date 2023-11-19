using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(Collider))]

public class MovementPhysics : MonoBehaviour
{
    //position where you press on screen
    private Vector3 mousePressDownPos;
    //position where you release on screen
    private Vector3 mouseReleasePos;

    //force of the shoot
    private float forceMultiplier = 60f;


    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// collects the position where you press on screen
    /// </summary>
    private void OnMouseDown()
    {
        mousePressDownPos = Input.mousePosition;
        Debug.Log("Hola");
    }

    /// <summary>
    /// collects the position where you release on screen and shoot
    /// </summary>
    private void OnMouseUp()
    {
        mouseReleasePos = Input.mousePosition;
        Debug.Log("Adios");
        //direction where the shot will go
        Vector3 direction = mouseReleasePos - mousePressDownPos;
        Shoot(direction);
    }

    

    //shooting with force on Y axis
    void Shoot(Vector3 Force)
    {
        Debug.Log("disparo!");
        //si quieres que sea misma fuerza todos los tiros
        rb.AddForce(new Vector3(-Force.x, -Force.y,0).normalized * forceMultiplier);
        //si quieres que se pueda recargar y cuanto mas tirachinas mas fuerte
        //rb.AddForce(new Vector3(-Force.x, -Force.y, 0) * forceMultiplier);
    }
}
