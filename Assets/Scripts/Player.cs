using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private GameObject _respawnPoint;
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

        transform.position = _respawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;

        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _speed;

        //if grounded
        if (_controller.isGrounded || (transform.position == _respawnPoint.transform.position))
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
        else if (transform.position.y <= -9f){
            LoseLife();
            Respawn();
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
        //hacky check for the position -> controller.Move is using the old position of the player before it respawns (?)
        if(transform.position != _respawnPoint.transform.position)
        {
            _controller.Move(velocity * Time.deltaTime);
        }
        

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
        _yVelocity = 0;
        Respawn();

        if(_lives < 1)
        {
            _lives = 0;
            _uiManager.UpdateLivesDisplay(_lives);
            //restart the game
            SceneManager.LoadScene(0);
        }
    }
    public void Respawn()
    {
        transform.position = _respawnPoint.transform.position;
    }

}
