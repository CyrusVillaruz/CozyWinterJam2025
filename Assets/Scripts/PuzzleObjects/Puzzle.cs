using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public Door door;
    public List<GameObject> gameObjects = new List<GameObject>();
    public int enemies = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (conditionCheck())
        {
            door.unlock();
        }
    }

    private bool conditionCheck()
    {
        foreach (GameObject puzzleobj in gameObjects)
        {
            if (!puzzleobj.GetComponent<PuzzleCondition>().fullfilled)
            {
                return false;
            }
        }
        if (enemies == 0)
        {
            return true;
        }
        return false;
    }
}
