using UnityEngine;

public class Fireball : MonoBehaviour
{

    public float speed;
    public float lifeTime;
    public float damage;
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



        if (collision.CompareTag("Enemy"))
        {
            EnemyController enemyRef = collision.gameObject.GetComponent<EnemyController>();
            if (enemyRef.canDamage) enemyRef.TakeDamage(damage);
            Debug.Log("Collided with enemy");
            Destroy(gameObject);
        }
    }
}
