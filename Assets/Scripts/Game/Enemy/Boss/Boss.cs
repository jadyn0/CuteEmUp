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

        if (bossHealth.health / bossHealth.maxHealth >= 0.5)
        {
            float attackChance = Random.Range(0, 50);
            if (attackChance <= 35f)
            {
                //spit
                Debug.Log("Spit");
            }
            if (attackChance > 35f)
            {
                //charge
                Debug.Log("Charge");
            }
        }

        if ( 0.5f > bossHealth.health / bossHealth.maxHealth)
        {
            if (bossHealth.health / bossHealth.maxHealth > 0.33f)
            {
                float attackChance = Random.Range(0, 70);
                if (attackChance <= 35f)
                {
                    //spit
                    Debug.Log("Spit");
                }
                if (35f < attackChance)
                {
                    if (attackChance < 50f)
                    {
                        //charge
                    Debug.Log("Charge");
                    }
                    else
                    {
                        //spawn
                        Debug.Log("Spawn");
                    }
                }
            }
            else
            {
                float attackChance = Random.Range(0, 80);
                if (attackChance <= 35f)
                {
                    //spit
                    Debug.Log("Spit");
                }
                if (35f < attackChance)
                {
                    if (attackChance < 50f)
                    {
                        //charge
                    Debug.Log("Charge");
                    }
                    if (50f < attackChance)
                    {
                        if (attackChance < 70f)
                        {
                            //spawn
                            Debug.Log("Spawn");
                        }
                        else
                        {
                            //beam
                            Debug.Log("Beam");
                        }
                    }
                }
            }
        }
    }
}
