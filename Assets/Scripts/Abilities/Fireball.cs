using UnityEngine;

public class Fireball : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Torch torch = collision.GetComponent<Torch>();

        if (torch != null )
        {
            torch.lightUp();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
