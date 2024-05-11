using UnityEngine;
using System.Collections;

public class BaseEnemyAttack : MonoBehaviour
{
    public EnemyHand enemyHand;
    public int attackDamage = 10;


    protected virtual void Start()
    {
        if (enemyHand != null) {
            enemyHand.damage = attackDamage;
        }
    }
}