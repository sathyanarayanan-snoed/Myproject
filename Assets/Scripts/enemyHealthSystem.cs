using UnityEngine;

public class enemyHealthSystem : MonoBehaviour
{
    public int HP = 1000;
    private Rigidbody2D RB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            gameObject.SetActive(false);
        }
    }
   public void TakeDamage(int Damage)
    {
        HP -= Damage;
    }
}
