using System.Diagnostics;
using UnityEngine;
using System.Collections;

public class Shopkeeper : MonoBehaviour
{
    public Transform player;
    public float interactionDistance = 1f;
    public GameObject shopUI;
    public GameObject noShop;
    private bool isShowing;

    void Awake()
    {
        noShop.SetActive(false);
        isShowing = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!isShowing)
            {
                if (Vector3.Distance(player.position, transform.position) <= interactionDistance)
                {
                    ToggleShopUI(true);
                    isShowing = true;
                    Time.timeScale = 0;
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
        Time.timeScale = isShowing ? 0 : 1;
    }

    void ToggleShopUI(bool show)
    {
        if (shopUI != null)
        {
            shopUI.SetActive(show);
        }
        else
        {
            UnityEngine.Debug.LogError("Shop UI GameObject not assigned!");
        }
    }

    IEnumerator showNoShop()
    {
        noShop.SetActive(true);
        yield return new WaitForSeconds(1);
        noShop.SetActive(false);
    }
}
