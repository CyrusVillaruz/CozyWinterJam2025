using UnityEngine;

public class Torch : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool lit = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
     
    }

    public void lightUp()
    {
        if (!lit) 
        { 
            lit = true;
            animator.SetFloat("Lit", 1);
            this.GetComponent<PuzzleCondition>().fulfill();
        }
    }

    public void extinguish()
    {
        if (lit)
        {
            lit = false;
            animator.SetFloat("Lit", 0);
            this.GetComponent<PuzzleCondition>().unfulfill();
        }
    }
}
