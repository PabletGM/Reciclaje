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

    private float velocidadRotacion = 8f;

    [SerializeField]
    private GameObject canvasPlayer;

    [SerializeField]
    private GameObject panelWin;

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


    public void UpdateTextScore(int totalScore)
    {
        textScore.text = "Score " + totalScore;
    }

    public void UpdateTimer(float totalTimer)
    {
        string formattedTimer = totalTimer.ToString("F2");
        timer.text = "Time: " + formattedTimer;
    }

    // Start is called before the first frame update
    void Start()
    {
        textScore.text = "Score: 0";

    }

    //changes or flips camera rotation
    public void ComenzarCorrutinaCamara()
    {
        //invocamos metodo que activa indicaciones durante 4 segundos
        IndicacionesActivas();
        StartCoroutine(CamaraRotation()); 
    }

    //the tutorial of the trayectory or your finger to draw the drag each 4 seconds
    public void IndicacionesActivas()
    {
        canvasPlayer.SetActive(true);
        Invoke("DesactivarIndicacionesActivas", 4f);
    }


    public void DesactivarIndicacionesActivas()
    {
        canvasPlayer.SetActive(false);

    }




    // Update is called once per frame
    void Update()
    {
        
    }

    //camera rotatuion
    private IEnumerator CamaraRotation()
    {
        AudioManagerReciclaje.instance.PlaySFX("cameraGiro");
        Debug.Log("Efecto camara iniciado!");
       
        // Calcula la rotaci�n final sumando 180 grados al eje Z
        Quaternion rotacionFinal = cam.transform.rotation * Quaternion.Euler(0, 0, 180);
        // Utiliza Lerp para interpolar suavemente entre la rotaci�n actual y la rotaci�n final
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / velocidadRotacion;

            // Utiliza Lerp para aplicar una rotaci�n gradual
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, rotacionFinal, t);

            // Pausa hasta el pr�ximo frame para evitar bloquear la ejecuci�n del juego
            // y permitir que la rotaci�n sea suave
            yield return null;
        }

        // Aseg�rate de que la rotaci�n final sea exacta
        cam.transform.rotation = rotacionFinal;

        Debug.Log("Efecto camara acabado!");
    }

    //set win panel
    public void SetWinPanel(bool set)
    {
        GameController.instance.GetPlayerGameObject().SetActive(false);
        panelWin.SetActive(set);
    }
   
}
