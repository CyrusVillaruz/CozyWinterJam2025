using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // For future reference, consider moving this to a "GameManager" singleton instance
    // managing the game elements instead
    [SerializeField] private PlayerController _playerController;
    
    [SerializeField] private Transform player;
    [SerializeField] private Slider healthBar;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isPlayerDetected = false;
    private bool hasBoostedStats = false;

    public float maxHealth;
    public float currentHealth;
    public float knockbackForce;
    public float damage;
    public float movementSpeed;
    public float iframeDuration;
    public bool canDamage = true;
    public float movementSpeedBoost = 10;
    public float damageBoost = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (currentHealth <= (maxHealth / 2) && !hasBoostedStats)
        {
            movementSpeed += movementSpeedBoost;
            damage += damageBoost;
            hasBoostedStats = true;
        }

        healthBar.value = currentHealth;
    }

    private void FixedUpdate()
    {
        Vector2 toPlayer = player.position - transform.position;
        toPlayer.Normalize();

        if (isPlayerDetected) Move(toPlayer);
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
        StartCoroutine(InvincibilityCountdown());
    }

    private IEnumerator InvincibilityCountdown()
    {
        canDamage = false;
        for (float i = 0f; i < iframeDuration; i += Time.deltaTime)
        {
            sr.enabled = !sr.enabled;
            yield return null;
        }

        sr.enabled = true;
        canDamage = true;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) DestroyEnemy();
        StartCoroutine(InvincibilityCountdown());
    }

    private void DestroyEnemy()
    {
        _playerController.numWolvesSlain++;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerDetected = false;
        }
    }
}
