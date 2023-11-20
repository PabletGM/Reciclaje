using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField]
    private GameObject player;

    public Camera cam;

    //array de objetos que sean puntos respawneables donde haya basura
    [SerializeField]
    GameObject[] respawns;

    [SerializeField]
    private GameObject basura;

    private int totalScore =0;

    private int scoreToAdd = 2;

    private int totalTimeNormal = 10;


    private int actualTime = 0;

    private float velocidadRotacion = 0.01f;


    private bool timerAcabado = false;




    //singleton
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }


    public int GetActualTime()
    {
        return actualTime;
    }

    public int GetTotalTimeNormal()
    {
        return totalTimeNormal;
    }



    //a�ade 1 segundo al cronometro actual
    public void AddActualTime(int second)
    {
        actualTime += second;
    }

    //reinicia timer
    public void ReiniciarTime()
    {
        actualTime =0;
        UIManager.instance.UpdateTimer(0);
    }



    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //pones basura en todos los spots o position
    public void AparecerBasura()
    {
        //iniciamos basura en todos los spots
        for (int i = 0; i < respawns.Length; i++)
        {
            respawns[i].transform.Rotate(new Vector3(-90, 0, 0));
            Instantiate(basura, respawns[i].transform.position, Quaternion.identity);  
        }
        
    }

    public void SumarPuntuacion()
    {
        //se a�ade puntuacion
        totalScore+= scoreToAdd;
        //se pone por pantalLa
        UIManager.instance.UpdateTextScore(totalScore);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator EfectoGirarCamara()
    {
        Debug.Log("Efecto camara iniciado!");
        // Le sumas 180 en el eje Z
        // Calcula la rotaci�n final sumando 180 grados al eje Z
        Quaternion rotacionFinal = cam.transform.rotation * Quaternion.Euler(0, 0, 180);
        // Utiliza Lerp para interpolar suavemente entre la rotaci�n actual y la rotaci�n final
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * velocidadRotacion;

            // Utiliza Lerp para aplicar una rotaci�n gradual
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, rotacionFinal, t);

            // Pausa hasta el pr�ximo frame para evitar bloquear la ejecuci�n del juego
            // y permitir que la rotaci�n sea suave
            yield return null;
        }

        // Aseg�rate de que la rotaci�n final sea exacta
        cam.transform.rotation = rotacionFinal;

        Debug.Log("Efecto camara acabado!");
        StopAllCoroutines();
        UIManager.instance.ComenzarCorrutinaTimer();



    }

    public bool GetTimerAcabado()
    {
        return timerAcabado;
    }

    public void SetTimerAcabado(bool set)
    {
        timerAcabado = set;
        //si ha acabado timer lo reiniciamos
        if(timerAcabado)
        {
            //se reinicia contador a 0
            ReiniciarTime();
            //efecto girar camara
            StartCoroutine(EfectoGirarCamara());
            //timerAcabado = false
            timerAcabado = false;
        }
    }

    public GameObject GetPlayerGameObject()
    {
        return player;
    }


}
