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



    //array de objetos que sean puntos respawneables donde haya basura
    [SerializeField]
    GameObject[] respawns;

    [SerializeField]
    private GameObject basura;

    private int totalScore =0;

    private int scoreToAdd = 2;

    private int totalTimeNormal = 10;


    private int actualTime = 0;




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



    //añade 1 segundo al cronometro actual
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
        //se añade puntuacion
        totalScore+= scoreToAdd;
        //se pone por pantalLa
        UIManager.instance.UpdateTextScore(totalScore);
    }

    // Update is called once per frame
    void Update()
    {

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
            //timerAcabado = false
            timerAcabado = false;
            UIManager.instance.ComenzarCorrutinaTimer();
        }
    }

    public GameObject GetPlayerGameObject()
    {
        return player;
    }


}
