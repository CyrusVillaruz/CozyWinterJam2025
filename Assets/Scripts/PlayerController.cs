using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;
    private Vector2 movementInput;
    private float acceleration;

    public float moveSpeed;
    public float maxHealth, maxMana;
    public float currentHealth, currentMana;

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    public void OnFire()
    {
        Debug.Log("Attack");
    }
    
    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        acceleration = moveSpeed;

        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    
    private void Update()
    {

    }

    private void FixedUpdate()
    {
        rigidBody2D.linearVelocity = movementInput * acceleration;

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
}
