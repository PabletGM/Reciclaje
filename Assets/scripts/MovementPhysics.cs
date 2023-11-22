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

        private float maxDistance = 30;

        private Rigidbody rb;

        public float raycastDistance = 0.1f; // Distancia del rayo hacia abajo
        public LayerMask groundLayer; // Capa que representa el suelo
        private bool isGrounded;
        private bool canShoot = false;

        #endregion


    #region Mobile
        public float power =2f;
        public float maxDrag = 40f;

        Vector3 dragStartPos;
        Touch touch;



        #endregion

    private void Update()
    {
        #region Ordenador
        // Lanza un rayo hacia abajo desde la posición del jugador centrica
        
        // Lanza otros 2 raycast en las esquinas del player para que lo pille
        bool raycast1 = Physics.Raycast(new Vector3(transform.position.x + 0.6f, transform.position.y, transform.position.z), Vector3.down, raycastDistance, groundLayer);
        bool raycast2 = Physics.Raycast(new Vector3(transform.position.x - 0.6f, transform.position.y, transform.position.z), Vector3.down, raycastDistance, groundLayer);
        bool raycast3 = Physics.Raycast(transform.position, Vector3.down, raycastDistance, groundLayer);

        if(raycast1 || raycast2 || raycast3)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        

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
        //if (Application.platform == RuntimePlatform.Android)
        //{
            if(Input.touchCount > 0 && canShoot)
            {
                //Debug.Log("detectar input");
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
        //}
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
            AudioManagerReciclaje.instance.PlaySFX("recargar");
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
                AudioManagerReciclaje.instance.StopSFX();
                AudioManagerReciclaje.instance.PlaySFX("saltar");
                Shoot(direction, distance);
            }
        }
        #endregion

    }



    //shooting with force on Y axis
    void Shoot(Vector3 Force, float distance)
    {

        //si quieres que sea misma fuerza todos los tiros
        //rb.AddForce(new Vector3(-Force.x, -Force.y, 0).normalized * forceMultiplier);
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
        //this.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos = touch.position;
        dragStartPos.z = 0;
        //Debug.Log(dragStartPos);
        AudioManagerReciclaje.instance.PlaySFX("recargar");


    }

    private void Dragging()
    {
        //this.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        Vector3 draggingPos = touch.position;
        draggingPos.z = 0f;
        //Debug.Log(touch.position);

        Vector3 forceInit = (draggingPos - dragStartPos);
        Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.y));



        float distance = Vector3.Distance(dragStartPos, draggingPos)/10;
        Debug.Log(forceV);
        if (distance <= (maxDistance-10) && canShoot)
        {
            Debug.Log("Renderizaste");
            DrawTrajectory.Instance.UpdateTrajectory(forceV/1.5f, rb, Vector3.zero);
        }
        else
        {
            DrawTrajectory.Instance.HideLine();
        }
    }
    private void DragRelease()
    {
        DrawTrajectory.Instance.HideLine();
        //Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        Vector3 dragReleasePos = touch.position;
        //Debug.Log(dragReleasePos);
        dragReleasePos.z = 0f;

        Vector3 force = (dragStartPos - dragReleasePos)/10;
        //Debug.Log(force);
        Vector3 clampedforce = Vector3.ClampMagnitude(force, maxDrag) * power;

        //Vector3 clampedforce = force * power;
        /*Debug.Log(clampedforce)*/;

        AudioManagerReciclaje.instance.StopSFX();
        AudioManagerReciclaje.instance.PlaySFX("saltar");
        rb.AddForce(clampedforce);
        //Debug.Log(-clampedforce);
        //this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        
    }
    #endregion
}
