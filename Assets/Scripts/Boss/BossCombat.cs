using UnityEngine;
using UnityEngine.InputSystem;

public class BossCombat : BossBase
{
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private float tapDamage = 10f;

    void OnEnable()
    {
        actions.Default.Attack.performed += OnTouch;
        actions.Enable();
    }

    void OnDisable()
    {
        actions.Default.Attack.performed -= OnTouch;
        actions.Disable();
    }

    public override void OnTouch(InputAction.CallbackContext ctx)
    {
        base.OnTouch(ctx);
    }

    public override void OnRaycastHit(RaycastHit hit)
    {
        Debug.Log("Boss hit!");
        healthComponent.TakeDamage(tapDamage);
        healthComponent.ShowDamageNumber(tapDamage, Color.red);
        Debug.Log("Boss Current Health: " + healthComponent.CurrentHealth);
    }
}
