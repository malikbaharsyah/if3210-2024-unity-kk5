using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour
{
    protected float timeToCollect = 20f;

	protected virtual void Start()
    {
        Invoke("RemoveOrb", timeToCollect);
    }

	protected void Update()
	{
		transform.localRotation = Quaternion.Euler(90f, Time.time * 100f, 0f);
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && other is CapsuleCollider)
		{
			PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
			Debug.Log("Orb collected by player: " + playerHealth.currentHealth);
			CollectOrb(other.gameObject);
			Destroy(gameObject);
		}
	}

    protected virtual void CollectOrb(GameObject player)
    {
        
    }

    protected void RemoveOrb()
    {
        Destroy(gameObject);
    }
}