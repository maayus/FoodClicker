using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Baker")]
    public ShopButton bakerButton;
    public float bakerPrice = 10;
    public int bakerCount = 0;

    public int cpb = 1;
    public float cookTime = 1;

    private Clicker clicker;

    private void Start() 
    {
        clicker = FindObjectOfType<Clicker>();
        InvokeRepeating("Cook", 0, cookTime);
    }

    public void BuyBaker()
    {
        var realPrice = (int)Mathf.Ceil(bakerPrice);
        if (clicker.clicks >= realPrice)
        {
            clicker.clicks -= realPrice;
            UiManager.instance.UpdateClicks(clicker.clicks);

            bakerPrice *= 1.15f; // 15% increase
            realPrice = (int)Mathf.Ceil(bakerPrice);
            bakerButton.UpdateText(realPrice, ++bakerCount);
        }
    }
    public void Cook()
    {
        var particleCount = Mathf.Min(bakerCount * cpb, 100);
        clicker.clickParticles.Emit(particleCount);

        clicker.clicks += bakerCount * cpb;
        UiManager.instance.UpdateClicks(clicker.clicks);
    }
}
