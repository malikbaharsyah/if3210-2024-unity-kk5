using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyController : MonoBehaviour
{
    public GameObject dragonPrefabs;
    public GameObject griffinPrefabs;
    public ScoreManager scoreManager;
    public GameObject notEnough;
    private TextMeshProUGUI priceText;
    private int price;
    private int currentCoin;
    Transform shopKeeper;

    void Awake()
    {
        notEnough.SetActive(false);
        priceText = GetComponent<TextMeshProUGUI>();
        shopKeeper = GameObject.FindGameObjectWithTag("Shopkeeper").transform;
    }
    public void BuyGriffin()
    {
        currentCoin = scoreManager.getScore();
        price = 100;
        if (currentCoin >= price)
        {
            GameObject pet = Instantiate(griffinPrefabs, shopKeeper.position, Quaternion.identity);
            scoreManager.setScore(currentCoin - price);
        } else
        {
            notEnough.SetActive(true);
        }
    }

    public void BuyDragon()
    {
        currentCoin = scoreManager.getScore();
        price = 100;
        if (currentCoin >= price)
        {
            GameObject pet = Instantiate(dragonPrefabs, shopKeeper.position, Quaternion.identity);
            scoreManager.setScore(currentCoin - price);
        } else
        {
            notEnough.SetActive(true);
        }
    }

    IEnumerator ShowNoMoney()
    {
        notEnough.SetActive(true);
        yield return new WaitForSeconds(1);
        notEnough.SetActive(false);
    }
}
