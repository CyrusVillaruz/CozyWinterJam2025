using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damage;
    public bool attacking = false;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update () 
    {
        if (attacking == true) 
        {
            animator.SetFloat("Blend", 1);
        }
        else
        {
            animator.SetFloat("Blend", 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && attacking)
        {
            EnemyController enemyRef = collision.gameObject.GetComponent<EnemyController>();
            if (enemyRef.canDamage) enemyRef.TakeDamage(damage);
            Debug.Log("Collided with enemy");
        }
    }
}
