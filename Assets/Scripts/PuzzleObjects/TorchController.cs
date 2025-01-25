using UnityEngine;

public class Torch : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
     
    }
}
