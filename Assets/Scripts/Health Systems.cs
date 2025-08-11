using UnityEngine;
using UnityEngine.UIElements;

public class HealthSystems : MonoBehaviour
{

    private float HP = 1000;
    private Rigidbody2D RB;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (HP == 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HP -= 100;
           
        }

    }


    void TakeDamage(int Damage)
    {
        HP -= 150;
    }
    

    
}
