using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    private PlayerController _player;
    public Ability ability;
    public KeyCode key;
    
    float cooldown;
    float duration;

    enum AbilityState {
        Ready,
        Active,
        Cooldown
    }

    AbilityState currentState = AbilityState.Ready;

    void Start()
    {
        _player = GetComponent<PlayerController>();
    }

    void Update()
    {
        switch (currentState)
        {
            case AbilityState.Ready:
                if (Input.GetKeyDown(key) && _player.currentMana >= ability.manaCost)
                {
                    ability.Activate(gameObject);
                    currentState = AbilityState.Active;
                    duration = ability.duration;
                }
            break;
            case AbilityState.Active:
                if (duration > 0)
                {
                    duration -= Time.deltaTime;
                }
                else
                {
                    ability.BeginCooldown(gameObject);
                    currentState = AbilityState.Cooldown;
                    cooldown = ability.cooldown;
                }
            break;
            case AbilityState.Cooldown:
                if (cooldown > 0)
                {
                    cooldown -= Time.deltaTime;
                }
                else
                {
                    currentState = AbilityState.Ready;
                }
                break;
        }
    }
}
