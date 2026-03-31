using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Behavior;
using TMPro;

public class BossCombat : BossBase
{
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private float tapDamage = 10f;
    [SerializeField] private BehaviorGraphAgent agent;
    [SerializeField] private TMP_Text statusText;

    private float enragedHealthThreshold = 0.35f;

    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<BehaviorGraphAgent>();
        }

        DieEvent += () => { agent.SetVariableValue<bool>("isDead", true); };
        StunEvent += () => { agent.SetVariableValue<bool>("isStunned", true); };
        EnrageEvent += () => { agent.SetVariableValue<bool>("isEnraged", true); };
    }

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

        if (!DeathCheck()) EnrageCheck();

        Debug.Log("Boss Current Health: " + healthComponent.CurrentHealth);
    }

    public void Attack()
    {
        AttackEvent?.Invoke();
        statusText.text = "Attacking!";
    }

    public void EnrageCheck()
    {
        float healthPercentage = healthComponent.GetHealthPercentage();
        if (healthPercentage <= enragedHealthThreshold)
        {
            EnrageEvent?.Invoke();
            statusText.text = "Enraged!";
        }
    }

    public bool DeathCheck()
    {
        if (healthComponent.CurrentHealth <= 0)
        {
            DieEvent?.Invoke();
            statusText.text = "Defeated!";
            return true;
        }
        return false;
    }
}
