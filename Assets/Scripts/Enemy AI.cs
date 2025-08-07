using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

    public float attackInterval = 2.0f;
    public float attackRange = 1.0f;
    public Transform player;
    public GameObject attackObject;
    private bool isAttacking = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(AttackPlayer());
    }

    IEnumerator AttackPlayer()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                Attack();
            }

            yield return new WaitForSeconds(attackInterval);

        }
    }

    void Attack()
    {
         if (!isAttacking)
    {
        isAttacking = true;

        GameObject attack = Instantiate(attackObject, transform.position, Quaternion.identity);

        StartCoroutine(MoveAttack(attack));  

        Invoke(nameof(ResetAttack), 0.5f);
    }
    }

     IEnumerator MoveAttack(GameObject attack)
    {
         while (Vector2.Distance(attack.transform.position, player.position) > 0.1f)
    {
        attack.transform.position = Vector2.MoveTowards(attack.transform.position, player.position, 5f * Time.deltaTime);
        yield return null;
    }
    Destroy(attack);
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

   
}
