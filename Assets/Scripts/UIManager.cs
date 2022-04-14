using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _coinCountText, _livesCountText;
    
    public void UpdateCoinDisplay(int coins)
    {
        _coinCountText.text = "Coins: " + coins.ToString();
    }

    public void UpdateLivesDisplay(int lives)
    {
        _livesCountText.text = "Lives: " + lives.ToString();
    }
}
