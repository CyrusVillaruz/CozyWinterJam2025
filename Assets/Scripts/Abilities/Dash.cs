using UnityEngine;

[CreateAssetMenu]
public class Dash : Ability
{
    public float dashVelocity;

    public override void Activate(GameObject parent)
    {
        PlayerController player = parent.GetComponent<PlayerController>();

        if (player.movementInput.magnitude != 0f)
        {
            player.acceleration = dashVelocity;
            player.ConsumeMana(manaCost);
        }
    }

    public override void BeginCooldown(GameObject parent)
    {
        PlayerController player = parent.GetComponent<PlayerController>();
        player.acceleration = player.moveSpeed;
    }
}
