using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    [HideInInspector] public float acceleration;
    [HideInInspector] public Vector2 movementInput;
    [HideInInspector] public float currentHealth, currentMana;

    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;
    private Vector2 noMovement = new Vector2(0, 0);
    private Animator animator;

    [Header("Player Data")]
    public PlayerBars healthBar;
    public PlayerBars manaBar;
    public float moveSpeed;
    public Vector2 faceDirection;
    public Vector2 moveDirection;
    public float maxHealth, maxMana;
    public float healthRegenRate, manaRegenRate;
    public float knockbackForce;

    [Header("Fireball")]
    public Rigidbody2D rotator;
    public Transform shootingPoint;
    public GameObject fireballPrefab;
    public float fireballManaCost;

    [Header("I-Frames")]
    public float iframeDuration;
    public bool isInvincible = false;
    public bool canDamage = true;

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
        if (movementInput != noMovement) 
        { 
            faceDirection = movementInput; 
            moveDirection = movementInput * 2;
        }
    }

    public void OnFire()
    {
        Debug.Log("Attack");
    }
    
    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        acceleration = moveSpeed;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }


    private void Update()
    {
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        else currentHealth += healthRegenRate * Time.deltaTime;
        healthBar.SetHealth(currentHealth);

        if (currentMana > maxMana) currentMana = maxMana;
        else currentMana += manaRegenRate * Time.deltaTime;
        manaBar.SetMana(currentMana);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Instantiate(fireballPrefab, shootingPoint.position, shootingPoint.rotation);
            ConsumeMana(fireballManaCost);
        }
    }

    private void OnClick()
    {
        StartCoroutine(Slash());
    }

    private IEnumerator Slash()
    {
        shootingPoint.GetComponent<Sword>().attacking = true;
        yield return new WaitForSeconds(0.3f);
        Debug.Log("Attack");
        shootingPoint.GetComponent<Sword>().attacking = false;
    }

    private void FixedUpdate()
    {
        rigidBody2D.linearVelocity = movementInput * acceleration;
        rotator.transform.position = this.transform.position;
        if (movementInput == noMovement) 
        {
            animator.SetFloat("MoveX", faceDirection.x);
            animator.SetFloat("MoveY", faceDirection.y);
        }
        else
        {
            animator.SetFloat("MoveX", moveDirection.x);
            animator.SetFloat("MoveY", moveDirection.y);
        }

        FaceMouse();
    }

    // Custom method to flip the sprite towards the mouse position, if needed
    private void FaceMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x -= objectPos.x;
        if ((mousePos.x > 0 && faceDirection.x < 0) || (mousePos.x < 0 && faceDirection.x > 0))
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public void ConsumeMana(float mana)
    {
        currentMana -= mana;
    }

    public void TakeDamage(float damage, Vector2 knockbackDirection)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        rigidBody2D.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        if (currentHealth <= 0)
        {
            // Restart the level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        StartCoroutine(InvincibilityCountdown());
    }

    private IEnumerator InvincibilityCountdown()
    {
        canDamage = false;
        for (float i = 0f; i < iframeDuration; i += Time.deltaTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return null;
        }

        spriteRenderer.enabled = true;
        canDamage = true;
    }
}
