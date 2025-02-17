using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public float cooldown;
    public float duration;
    public int manaCost;

    public virtual void Activate(GameObject parent) {}
    public virtual void BeginCooldown(GameObject parent) {}
}
