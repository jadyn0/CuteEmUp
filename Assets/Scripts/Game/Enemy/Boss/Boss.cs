using UnityEngine;
using System.Collections;


public class Boss : MonoBehaviour
{
    public float delayLowerBound;
    public float delayUpperBound;

    public BossHealth bossHealth;

    public void Attack()
    {
        StartCoroutine(AttackDelay());
    }
    IEnumerator AttackDelay()
    {
        float delay = Random.Range(delayLowerBound, delayUpperBound);
        yield return new WaitForSeconds(delay);

        float attackUpper;
        if (bossHealth.health / bossHealth.maxHealth >= 0.5)
        {
            attackUpper = 2;
        }
        float AttackChance = Random.Range(0, 4);
        Debug.Log(AttackChance);
    }
}
