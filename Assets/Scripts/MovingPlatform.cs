using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _targetA, _targetB;
    private float _speed = 2.0f;
    private bool _returnToBase = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_returnToBase == false)
        {
            //move to target b
            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _speed * Time.deltaTime);
        }
        else if (_returnToBase)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _speed * Time.deltaTime);
        }
        //go to point b
        //if at point b
        if (transform.position == _targetB.position)
        {
            _returnToBase = true;
        }
        //  go to point a
        //if at point a
        if(transform.position == _targetA.position)
        {
            _returnToBase = false;
        }
        //  go to point b
    }
    //collision detection with player
    //if collide with player
    //take player parent == this game object
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    //exit collision
    //check if player exited
    //take the player parent = null
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
