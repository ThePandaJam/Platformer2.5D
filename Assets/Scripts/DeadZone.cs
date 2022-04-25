using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private bool _isPlayerDying = false;
    //check collision
    private void OnTriggerEnter(Collider other)
    {

        if(!_isPlayerDying && (other.tag == "Player"))
        {
            Debug.Log("Player entered Dead Zone");
            Player player = other.GetComponent<Player>();
            Debug.Log("y pos: " + player.transform.position.y);
            if (player != null)
            {
                player.LoseLife();
            }
            _isPlayerDying = true;
        }
    }
    
    private void OnTriggerExit(Collider other){
        Player player = other.GetComponent<Player>();
        _isPlayerDying = false;
        Debug.Log("y pos: " + player.transform.position.y);
        Debug.Log("player exiting dead zone");
    }
}
