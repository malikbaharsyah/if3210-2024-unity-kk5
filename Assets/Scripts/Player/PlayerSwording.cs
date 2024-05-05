using UnityEngine;
using System.Collections;

public class PlayerSwording : MonoBehaviour
{
	public bool canAttack = true;
	public bool isAttacking = false;
	public float attackCooldown = 0.5f;

	public void SwordAttack()
	{
		Debug.Log("Masuk");
		isAttacking = true;
		canAttack = false;
		Animator animator = GetComponent<Animator>();
		animator.SetTrigger("SwordAttack");
		StartCoroutine(ResetAttackCooldown());
	}

	IEnumerator ResetAttackCooldown()
	{
		StartCoroutine(ResetAttackBool());
		yield return new WaitForSeconds(attackCooldown);
		canAttack = true;
	}

	IEnumerator ResetAttackBool()
	{
		yield return new WaitForSeconds(1.0f);
		isAttacking = false;
	}
}