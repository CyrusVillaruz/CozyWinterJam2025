using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    private bool locked = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
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
            this.GetComponent<PuzzleCondition>().fulfill();
        }
    }

    public void lockDoor()
    {
        if (!locked)
        {
            locked = true;
            animator.SetFloat("Blend", 0f);
            this.GetComponent<PuzzleCondition>().unfulfill();
        }
    }

}
