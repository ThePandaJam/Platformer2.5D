using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //onTriggerEnter
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.CoinIncrement();
            }
            Destroy(this.gameObject);
        }
    }
    //increment coin count in player
    //notify UI via player
    //destroy coin object
}
