using System.Diagnostics;
using UnityEngine;
using System.Collections;

public class Shopkeeper : MonoBehaviour
{
    public int disappearTime;
    public GameObject shopKeeperObject;
    public Transform player;
    public Transform shopKeeper;
    public float interactionDistance = 1f;
    public GameObject shopUI;
    public GameObject noShop;
    private bool isShowing;

    void Awake()
    {
        shopKeeper = shopKeeperObject.transform;
        noShop.SetActive(false);
        isShowing = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(delayShop());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!isShowing)
            {
                if (Vector3.Distance(player.position, shopKeeper.position) <= interactionDistance && shopKeeperObject.activeInHierarchy)
                {
                    ToggleShopUI(true);
                    isShowing = true;
                }
                else
                {
                    StartCoroutine(showNoShop());
                }
            }
            else
            {
                ToggleShopUI(false);
                isShowing = false;             
            }
        }
    }

    void ToggleShopUI(bool show)
    {
        if (shopUI != null)
        {
            shopUI.SetActive(show);
        }
    }

    IEnumerator showNoShop()
    {
        noShop.SetActive(true);
        yield return new WaitForSeconds(1);
        noShop.SetActive(false);
    }

    IEnumerator delayShop()
    {
        shopKeeperObject.SetActive(true);
        yield return new WaitForSeconds(disappearTime);
        shopKeeperObject.SetActive(false);
        ToggleShopUI(false);
        isShowing = false;
    }
}
