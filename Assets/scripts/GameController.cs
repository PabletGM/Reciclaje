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

    public GameObject GetPlayerGameObject()
    {
        return player;
    }


}
