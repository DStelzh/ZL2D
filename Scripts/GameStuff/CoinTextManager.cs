using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CoinTextManager : MonoBehaviour
{
    public Inventory playerInventory;
    public TextMeshProUGUI coinDisplay;
   public void UpdateCoinText()
    {
        coinDisplay.text = "" + playerInventory.coins;
    }
}
