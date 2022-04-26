using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float _jumpHeight = 15.0f;
    private bool _canDoubleJump = false;

    private float _yVelocity;

    private UIManager _uiManager;
    private int _coinCount = 0;
    
    [SerializeField]
    private int _lives = 3;

    private Vector3 _respawnPoint;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _respawnPoint = new Vector3(-6.5f, 0, 0);

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }
        _uiManager.UpdateCoinDisplay(_coinCount);
        _uiManager.UpdateLivesDisplay(_lives);

        transform.position = _respawnPoint;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _speed;
        

        if (_controller.isGrounded)
        {
            //jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
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
    public void LoseLife()
    {
        _lives--;
        _uiManager.UpdateLivesDisplay(_lives);

        if(_lives < 1)
        {
            _lives = 0;
            _uiManager.UpdateLivesDisplay(_lives);
            //restart the game
            SceneManager.LoadScene(0);
        }
    }


}
