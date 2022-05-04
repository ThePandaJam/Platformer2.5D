using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _gravity = 1.0f;
    
    [SerializeField] private float _jumpHeight = 30.0f;
    private bool _canDoubleJump = false;

    private float _yVelocity;

    private UIManager _uiManager;
    private int _coinCount = 0;
    
    [SerializeField] private int _lives = 3;
    private bool _isPlayerDying = false;

    [SerializeField] private Vector3 _respawnPoint = new Vector3(-6.5f, 0, 0);
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

        //transform.position = _respawnPoint;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _speed;

        if (_controller.isGrounded || (transform.position == _respawnPoint))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else if (transform.position.y <= -9f){
            LoseLife();
            Respawn();
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
        
        /*if (!_isPlayerDying || (transform.position == _respawnPoint))
        { 
            _yVelocity -= _gravity;
        }*/
        //falling off world
        /*if (!_isPlayerDying && (transform.position.y <= -9f)){
            _yVelocity = 0;
            _isPlayerDying = true;
            Respawn();
            //LoseLife();
        }*/

        velocity.y = _yVelocity;
        if (transform.position != _respawnPoint)
        {
            _controller.Move(velocity * Time.deltaTime);
        }
    }

    public void CoinIncrement()
    {
        _coinCount++;
        _uiManager.UpdateCoinDisplay(_coinCount);
    }

    void LoseLife()
    {
        _lives--;
        _uiManager.UpdateLivesDisplay(_lives);
        _yVelocity = 0;

        Respawn();

        if(_lives < 1)
        {
            _lives = 0;
            _uiManager.UpdateLivesDisplay(_lives);
            SceneManager.LoadScene(0);
        }
        
    }
    void Respawn()
    {
        
        transform.position = _respawnPoint;
        
        //_isPlayerDying = false;
    }

}
