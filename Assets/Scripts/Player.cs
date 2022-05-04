using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//IMPORTANT NOTE: For proper functionality, ensure "Auto Sync Transforms" is ON:
// Edit > Project Settings > Physics
// check the box “Auto Sync Transforms”
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

        transform.position = _respawnPoint;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _speed;

        if (_controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else if (transform.position.y <= -9f){
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
        
        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
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
        
        if(_lives < 1)
        {
            _lives = 0;
            _uiManager.UpdateLivesDisplay(_lives);
            SceneManager.LoadScene(0);
        }
        
    }
    void Respawn()
    {
        LoseLife();
        transform.position = _respawnPoint;
    }

}
