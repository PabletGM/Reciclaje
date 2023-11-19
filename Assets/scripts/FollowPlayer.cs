using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Ajusta la velocidad de suavizado
    public float smoothSpeed = 1f; 

    //FollowPlayer con suavizado
    private void FixedUpdate()//update, lateupdate y fixedUpdate
    {
        BuscarPosicionPlayer();
    }

    private void BuscarPosicionPlayer()
    {

        //buscamos la posicion inicial del player
        GameObject player = GameController.instance.GetPlayerGameObject();
        if (player != null)
        {
            this.gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,this.gameObject.transform.position.z);

            Vector3 desiredPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Establece la posición de la cámara
            transform.position = smoothedPosition;
        }
       
    }
}
