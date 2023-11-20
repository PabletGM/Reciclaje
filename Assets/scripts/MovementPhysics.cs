using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(Collider))]

public class MovementPhysics : MonoBehaviour
{

    //if (Application.platform == RuntimePlatform.Android)
    //{
    //    Debug.Log("Do something special here!");
    //}

    //if (Application.platform == RuntimePlatform.Windows)
    //{
    //    Debug.Log("Do something special here!");
    //}

#region Ordenador

//position where you press on screen
private Vector3 mousePressDownPos;
    //position where you release on screen
    private Vector3 mouseReleasePos;

    //force of the shoot
    private float forceMultiplier =1f;

    private float maxDistance = 80;

    private Rigidbody rb;

    public float raycastDistance = 0.1f; // Distancia del rayo hacia abajo
    public LayerMask groundLayer; // Capa que representa el suelo
    private bool isGrounded;
    private bool canShoot = false;



    private void Update()
    {
        // Lanza un rayo hacia abajo desde la posición del jugador
        isGrounded = Physics.Raycast(transform.position, Vector3.down, raycastDistance, groundLayer);

        // Comprueba si el jugador está en el suelo
        if (isGrounded)
        {
            canShoot = true;
        }
        else
        {
            canShoot= false;
        }
    }

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

            float distance = Vector3.Distance(Input.mousePosition, mousePressDownPos) * forceMultiplier;
            //Debug.Log(distance);
        if (distance <= maxDistance && canShoot)
        {
            DrawTrajectory.Instance.UpdateTrajectory(forceV, rb, Vector3.zero);
        }
        else
        {
            DrawTrajectory.Instance.HideLine();
        }
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
        float distance = Vector3.Distance(mouseReleasePos, mousePressDownPos);
        //para que tenga permiso debe detectar un raycast que está en el suelo
        if(canShoot)
        {
            Shoot(direction, distance);
        }
        
    }

    

    //shooting with force on Y axis
    void Shoot(Vector3 Force, float distance)
    {

        //si quieres que sea misma fuerza todos los tiros
        //rb.AddForce(new Vector3(-Force.x, -Force.y,0).normalized * forceMultiplier);
        //si quieres que se pueda recargar y cuanto mas tirachinas mas fuerte

        
        //si supera limites de fuerza no tiramos
        
        if(distance <= maxDistance)
        {
            rb.AddForce(new Vector3(-Force.x, -Force.y, 0) * forceMultiplier);
        }
    }



    #endregion
}
