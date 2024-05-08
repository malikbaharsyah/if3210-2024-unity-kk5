using UnityEngine;
using System.Collections;

public class BaseEnemyAttack : MonoBehaviour
{
    public EnemyHand enemyHand;
    public int attackDamage = 10;


    void Start()
    {
        enemyHand.damage = attackDamage;
    }
}