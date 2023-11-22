using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectRubbish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //si el objeto con el que choca es player
        if(other.gameObject.tag == "player")
        {
            AudioManagerReciclaje.instance.PlaySFX("papel");
            //añadimos puntuacion al player
            GameController.instance.SumarPuntuacion();
            //desactivamos objeto
            this.gameObject.SetActive(false);
        }
    }
}
