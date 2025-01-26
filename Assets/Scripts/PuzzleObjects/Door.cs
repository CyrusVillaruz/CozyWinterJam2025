using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    private bool locked = true;
    private Collider2D doorCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void unlock()
    {
        if (locked)
        {
            locked = false;
            animator.SetFloat("Blend", 1f);
            doorCollider.enabled = false;
            this.GetComponent<PuzzleCondition>().fulfill();
        }
    }

    public void lockDoor()
    {
        if (!locked)
        {
            locked = true;
            animator.SetFloat("Blend", 0f);
            doorCollider.enabled = true;
            this.GetComponent<PuzzleCondition>().unfulfill();
        }
    }

}
