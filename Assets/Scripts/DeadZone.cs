using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private Vector3 _respawnPoint = new Vector3(-6.5f, 0, 0);

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

            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
            }
            other.transform.position = _respawnPoint;
            StartCoroutine(CCENableRoutine(cc));
        }
    }

    IEnumerator CCENableRoutine(CharacterController controller)
    {
        yield return new WaitForSeconds(0.5f);
        controller.enabled = true;
    }
}
