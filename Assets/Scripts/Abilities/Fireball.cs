using UnityEngine;

public class Fireball : MonoBehaviour
{

    public float speed;
    public float lifeTime;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Torch torch = collision.GetComponent<Torch>();

        if (torch != null )
        {
            torch.lightUp();
        }
    }
}
