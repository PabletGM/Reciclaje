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

        #endregion


    #region Mobile
        public float power = 10f;
        public float maxDrag = 5f;

        Vector3 dragStartPos;
        Touch touch;



        #endregion

    private void Update()
    {
        #region Ordenador
        // Lanza un rayo hacia abajo desde la posición del jugador
        isGrounded = Physics.Raycast(transform.position, Vector3.down, raycastDistance, groundLayer);

        // Comprueba si el jugador está en el suelo
        if (isGrounded)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
        #endregion

        #region Movil
        if (Application.platform == RuntimePlatform.Android)
        {
            if(Input.touchCount > 0)
            {
                //primer toque
                touch = Input.GetTouch(0);
                //empieza el toque
                if(touch.phase == TouchPhase.Began)
                {
                    DragStart();
                }
                if (touch.phase == TouchPhase.Moved)
                {
                    Dragging();
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    DragRelease();
                }
            }
        }
        #endregion
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    #region Ordenador

    /// <summary>
    /// collects the position where you press on screen
    /// </summary>
    private void OnMouseDown()
    {
        #region Ordenador
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            mousePressDownPos = Input.mousePosition;
        }
        #endregion
    }

    //se llama cuando se acaba de hacer el drag
    private void OnMouseDrag()
    {
        #region Ordenador

            if (Application.platform == RuntimePlatform.WindowsEditor)
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
        #endregion

    }

    /// <summary>
    /// collects the position where you release on screen and shoot
    /// </summary>
    private void OnMouseUp()
    {
        #region Ordenador
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            DrawTrajectory.Instance.HideLine();
            mouseReleasePos = Input.mousePosition;

            //direction where the shot will go
            Vector3 direction = mouseReleasePos - mousePressDownPos;
            float distance = Vector3.Distance(mouseReleasePos, mousePressDownPos);
            //para que tenga permiso debe detectar un raycast que está en el suelo
            if (canShoot)
            {
                Shoot(direction, distance);
            }
        }
        #endregion

    }



    //shooting with force on Y axis
    void Shoot(Vector3 Force, float distance)
    {

        //si quieres que sea misma fuerza todos los tiros
        //rb.AddForce(new Vector3(-Force.x, -Force.y,0).normalized * forceMultiplier);
        //si quieres que se pueda recargar y cuanto mas tirachinas mas fuerte
        #region Ordenador
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            //si supera limites de fuerza no tiramos

            if (distance <= maxDistance)
            {
                rb.AddForce(new Vector3(-Force.x, -Force.y, 0) * forceMultiplier);
            }
        }
        #endregion
    }

    #endregion

    #region Movil
    private void DragStart()
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        //dragStartPos.z = 0;
    }

    private void Dragging()
    {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        //draggingPos.z = 0f;
    }
    private void DragRelease()
    {
        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        //draggingPos.z = 0f;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedforce = Vector3.ClampMagnitude(force, maxDrag) * power;

        float distance = Vector3.Distance(dragStartPos, dragReleasePos) * power;
        //Debug.Log(distance);
        if (distance <= maxDistance && canShoot)
        {
            DrawTrajectory.Instance.UpdateTrajectory(clampedforce, rb, Vector3.zero);
        }
        else
        {
            DrawTrajectory.Instance.HideLine();
        }

        rb.AddForce(-clampedforce);
    }
    #endregion
}
