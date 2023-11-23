using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectRubbish : MonoBehaviour
{
   
    //the gift box collider activates the method
    private void OnTriggerEnter(Collider other)
    {
        //si el objeto con el que choca es player
        if(other.gameObject.tag == "player")
        {
            //music paper
            AudioManagerReciclaje.instance.PlaySFX("papel");
            //add puntuation to player
            GameController.instance.SumarPuntuacion();
            //gameObject desactivate
            this.gameObject.SetActive(false);
        }
    }
}
