using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    //check collision
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {
            Debug.Log("Player entered Dead Zone");
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.LoseLife();
            }
        }
    }

    IEnumerator CCENableRoutine(CharacterController controller)
    {
        yield return new WaitForSeconds(0.5f);
        controller.enabled = true;
    }
}
