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

    public Camera cam;

    private float velocidadRotacion = 1f;

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
        ComenzarCorrutinaTimer();
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
       
            //se hace mientras cron�metro no llegue al maximo, timerAcabado = false
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
                //si ha superado el tiempo m�ximo de 10 segundos reiniciamos tiempo, giramos camara
                else
                {
                    #region comportamientoCamara
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
                    #endregion
                    //al llegar al totalTime llamamos a metodo de GameManager
                    GameController.instance.SetTimerAcabado(true);
                    //parar corrutina
                    break;
                }

            }



    }

   
}
