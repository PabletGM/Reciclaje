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
    private float forceMultiplier = 2f;



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

    }

    //se llama cuando se acaba de hacer el drag
    private void OnMouseDrag()
    {
        //fuerza inicial
            Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
            Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.y)) * forceMultiplier;

        
            DrawTrajectory.Instance.UpdateTrajectory(forceV, rb, Vector3.zero);
    }

    /// <summary>
    /// collects the position where you release on screen and shoot
    /// </summary>
    private void OnMouseUp()
    {
        DrawTrajectory.Instance.HideLine();
        mouseReleasePos = Input.mousePosition;

        //direction where the shot will go
        Vector3 direction = mouseReleasePos - mousePressDownPos;
        Shoot(direction);
    }

    

    //shooting with force on Y axis
    void Shoot(Vector3 Force)
    {

        //si quieres que sea misma fuerza todos los tiros
        //rb.AddForce(new Vector3(-Force.x, -Force.y,0).normalized * forceMultiplier);
        //si quieres que se pueda recargar y cuanto mas tirachinas mas fuerte
        Debug.Log(new Vector3(-Force.x, -Force.y, 0) * forceMultiplier);
        rb.AddForce(new Vector3(-Force.x, -Force.y, 0) * forceMultiplier);
    }

   


}
