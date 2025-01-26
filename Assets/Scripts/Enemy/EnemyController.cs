using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;

    public float maxHealth;
    public float currentHealth;
    public float knockbackForce;
    public float damage;
    public float movementSpeed;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    protected virtual void FixedUpdate()
    {
        Vector2 toPlayer = player.position - transform.position;
        toPlayer.Normalize();

        Move(toPlayer);
    }

    protected virtual void Move(Vector2 toTarget)
    {
        bool shouldFlip = false;

        if (toTarget.x > 0 && sr.flipX) shouldFlip = true;
        else if (toTarget.x < 0 && !sr.flipX) shouldFlip = true;

        if (shouldFlip) sr.flipX = !sr.flipX;

        rb.AddForce(toTarget * movementSpeed * Time.deltaTime);
        // anim.SetBool("run", toTarget != Vector2.zero);
    }

    public virtual void TakeDamage(float damage, Vector2 knockbackDirection)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) DestroyEnemy();

        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }

    protected virtual void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.collider.GetComponent<Rigidbody2D>();
            Vector2 knockbackDirection = playerRb.transform.position - transform.position;
            knockbackDirection.Normalize();

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage, knockbackDirection);
        }
    }
}
