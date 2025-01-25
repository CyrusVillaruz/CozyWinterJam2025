using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [HideInInspector] public float acceleration;
    [HideInInspector] public Vector2 movementInput;
    [HideInInspector] public float currentHealth, currentMana;

    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;
    private Vector2 noMovement = new Vector2(0, 0);
    private Animator animator;

    public PlayerBars healthBar, manaBar;
    public float moveSpeed;
    public Vector2 faceDirection;
    public Vector2 moveDirection;
    public float maxHealth, maxMana;
    public float healthRegenRate, manaRegenRate;
    public Transform shootingPoint;
    public GameObject fireballPrefab;

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
        }
    }

    private void FixedUpdate()
    {
        rigidBody2D.linearVelocity = movementInput * acceleration;
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
        if (mousePos.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (mousePos.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void ConsumeMana(float mana)
    {
        currentMana -= mana;
    }
}
