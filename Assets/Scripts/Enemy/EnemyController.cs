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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        Vector2 toPlayer = player.position - transform.position;
        toPlayer.Normalize();

        Move(toPlayer);

        Debug.Log("Player PositionX: " + toPlayer.x + " Player PositionY: " + toPlayer.y);
    }

    private void Move(Vector2 toTarget)
    {
        rb.AddForce(toTarget * movementSpeed);

        anim.SetBool("isRight", toTarget.x > 0);
        anim.SetBool("isLeft", toTarget.x < 0);
    }

    public void TakeDamage(float damage, Vector2 knockbackDirection)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) DestroyEnemy();

        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }

    private void DestroyEnemy()
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
            if (player.canDamage) player.TakeDamage(damage, knockbackDirection);
        }
    }
}
