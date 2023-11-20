using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField]
    private TextMeshProUGUI textScore;

    [SerializeField]
    private TextMeshProUGUI timer;

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

    public void UpdateTextScore(int totalScore)
    {
        textScore.text = "Score " + totalScore;
    }

    public void UpdateTimer(int totalTimer)
    {
        timer.text = "Time: " + totalTimer;
    }

    // Start is called before the first frame update
    void Start()
    {
        textScore.text = "Score: 0";
        //empezar corrutina timer
        StartCoroutine(TimerFisicasNormales());
    }

    public void ComenzarCorrutinaTimer()
    {
        StartCoroutine(TimerFisicasNormales());
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TimerFisicasNormales()
    {
       
            //se hace mientras cronómetro no llegue al maximo, timerAcabado = false
            while (!GameController.instance.GetTimerAcabado())
            {
                //si el tiempo actual < tiempo total
                if (GameController.instance.GetActualTime() < GameController.instance.GetTotalTimeNormal())
                {
                    //se suma 1 al cronometro actual
                    GameController.instance.AddActualTime(1);
                    int actualTime = GameController.instance.GetActualTime();
                    //se pone por pantalla
                    UpdateTimer(actualTime);
                    //se espera 1 segundo hasta reiniciar proceso
                    yield return new WaitForSeconds(1f);
                }
                //si ha superado el tiempo máximo
                else
                {
                    //al llegar al totalTime llamamos a metodo de GameManager
                    GameController.instance.SetTimerAcabado(true);
                    //parar corrutina
                    StopAllCoroutines();
                    break;
                }

            }



    }

   
}
