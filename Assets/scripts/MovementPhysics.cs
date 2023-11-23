using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        private float forceMultiplier =0.5f;

        private float maxDistance = 30;
        private float maxDistanceOrdenador = 250;

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

        [SerializeField]
        private ParticleSystem vfx;



        #endregion

    //ground comprobation to access to the jump
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

    //drag
    private void OnMouseDrag()
    {
        #region Ordenador

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            //fuerza inicial
            Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
            Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.y)) * forceMultiplier;

            //distance from the start point and the last point
            float distance = Vector3.Distance(Input.mousePosition, mousePressDownPos) * forceMultiplier;
            //condition to draw trajectory if it´s on the ground
            if (distance <= maxDistanceOrdenador+40 && canShoot)
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
                SetParticleSystem(true);
                AudioManagerReciclaje.instance.StopSFX();
                AudioManagerReciclaje.instance.PlaySFX("saltar");
                Shoot(direction, distance);
            }
        }
        #endregion

    }

    //when you jump vfx
    private void SetParticleSystem(bool set)
    {
        //si es true
        if(set)
        {
            //activamos vfx
            vfx.Play();               
        }
        vfx.gameObject.SetActive(set);
    }

    //shooting with force on Y axis
    void Shoot(Vector3 Force, float distance)
    {

        
        #region Ordenador
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            //si supera limites de fuerza no tiramos
            Debug.Log(distance);
            if (distance <= maxDistanceOrdenador + 40)
            {
                Debug.Log("shot");
                rb.AddForce(new Vector3(-Force.x, -Force.y, 0) * forceMultiplier);
            }
        }
        #endregion
    }

    #endregion

    #region Movil

    //first touch position on mobile
    private void DragStart()
    {
        dragStartPos = touch.position;
        dragStartPos.z = 0;
        AudioManagerReciclaje.instance.PlaySFX("recargar");
    }

    //touch position while drag
    private void Dragging()
    {
        
        Vector3 draggingPos = touch.position;
        draggingPos.z = 0f;


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

    //last touch pos
    private void DragRelease()
    {
        DrawTrajectory.Instance.HideLine();
        Vector3 dragReleasePos = touch.position;
        dragReleasePos.z = 0f;

        Vector3 force = (dragStartPos - dragReleasePos)/10;
        Vector3 clampedforce = Vector3.ClampMagnitude(force, maxDrag) * power;
;
        SetParticleSystem(true);
        AudioManagerReciclaje.instance.StopSFX();
        AudioManagerReciclaje.instance.PlaySFX("saltar");
        rb.AddForce(clampedforce);

        
    }
    #endregion
}
