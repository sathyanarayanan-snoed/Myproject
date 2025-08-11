using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 1000;
    public float attackCooldown = 0.3f;
    public float nextAttackTime = 0f;
    public Animator animator;
    public GameObject AttackEffect1;
    public DASH dash;
    private SpriteRenderer spriteRenderer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        dash = GetComponent<DASH>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime && Input.GetKeyDown(KeyCode.Z))
        {
            attack();
            nextAttackTime = Time.time + attackCooldown;
        }

    }
    void attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.SendMessage("TakeDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void SpawnAttackEffect()
{
    if (AttackEffect1 == null || attackPoint == null) return;

    GameObject effect = Instantiate(AttackEffect1, attackPoint.position, attackPoint.rotation);

    effect.transform.SetParent(attackPoint);

        // Reset local transform for perfect alignment

        
        // Adjust this if needed to align precisely with hand
      Vector3 baseOffset = new Vector3(-0.05f, -0.25f, 0f); // Default offset for LEFT facing
if (dash.move > 0f) baseOffset.x *= -1; // Mirror offset for RIGHT facing
effect.transform.localPosition = baseOffset;

// ==== Flip sprite only when facing RIGHT ====
SpriteRenderer sr = effect.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.flipX = dash.facingRight;
        Destroy(effect, 0.45f); // Destroy after effect duration
}
}
