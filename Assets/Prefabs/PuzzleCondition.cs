using UnityEngine;

public class PuzzleCondition : MonoBehaviour
{
    public bool fullfilled;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fullfilled = false;
    }

    public void fulfill ()
    {
        if (!fullfilled)
        {
            fullfilled = true;
            Debug.Log("fulfilled");
        }
    }

    public void unfulfill () 
    {
        if (fullfilled)
        {
            fullfilled = false;
            Debug.Log("unfulfilled");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
