using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float _jumpHeight = 30.0f;

    private float _yVelocity;
    private bool _canDoubleJump = false;

    //variable for player coins
    private UIManager _uiManager;
    private int _coinCount = 0;
    
    [SerializeField]
    private int _lives = 3;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }
        _uiManager.UpdateCoinDisplay(_coinCount);
        _uiManager.UpdateLivesDisplay(_lives);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _speed;

        //if grounded
        if (_controller.isGrounded)
        {
            //do nothing, jump later
            //if space key pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
            //(assign y velocity to jump height)
        }
        else
        {
            //check for double jump
            //current _yVelocity += jumpheight
            if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
            {
                _yVelocity += _jumpHeight;
                _canDoubleJump = false;
            }
            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }

    public void CoinIncrement()
    {
        _coinCount++;
        //update ui with the coins
        _uiManager.UpdateCoinDisplay(_coinCount);
    }
}
