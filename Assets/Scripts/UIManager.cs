using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _coinCountText;
    //update coin display()
    void Start()
    {
        _coinCountText.text = "Coins: " + 0;
    }
    public void UpdateCoinDisplay(int coins)
    {
        _coinCountText.text = "Coins: " + coins;
    }
}
