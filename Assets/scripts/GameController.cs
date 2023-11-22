using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

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

    private int maxScore = 12;

    private int scoreToAdd = 2;

    private int totalTimeNormal = 10;


    private float actualTime = 0;




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


    public float GetActualTime()
    {
        return actualTime;
    }

    public float GetTotalTimeNormal()
    {
        return totalTimeNormal;
    }

    public void Win()
    {
        UIManager.instance.SetWinPanel(true);
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

    public void Restart()
    {
        SceneManager.LoadScene("ReciclajeGame");
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
        //si el tiempo actual < tiempo total
        if (GetActualTime() < GetTotalTimeNormal())
        {
            //se suma 1 al cronometro actual
            actualTime += Time.deltaTime;
            //se pone por pantalla
            UIManager.instance.UpdateTimer(actualTime);
            //se espera 1 segundo hasta reiniciar proceso
        }
        else
        {
            //al llegar al totalTime llamamos a metodo de GameManager
            SetTimerAcabado(true);
            //giramos camara
            UIManager.instance.ComenzarCorrutinaCamara();
        }

        //condicion de victoria
        ConditionVictory();

    }

    public void ConditionVictory()
    {
        if(totalScore >= maxScore)
        {
            Win();
        }
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
        }
    }

    public GameObject GetPlayerGameObject()
    {
        return player;
    }


}
