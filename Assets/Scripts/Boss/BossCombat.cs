using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Behavior;
using TMPro;
using UnityEngine.UI;

public class BossCombat : BossBase
{
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private float tapDamage = 10f;
    [SerializeField] private float bossAtkDamage = 10f;
    [SerializeField] private float bossSpAtkDamage = 20f;
    [SerializeField] private float attackCoolDown = 2f;
    [SerializeField] private float stunTimeSpan = 3f;
    [SerializeField] private float enragedDamageBoost = 1.5f;
    [SerializeField] private float enragedCooldownReduction = 0.8f;
    [SerializeField] private float enragedHealthThreshold = 0.35f;
    [SerializeField] private TMP_Text statusText;

    public BehaviorGraphAgent agent;
    
    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<BehaviorGraphAgent>();
        }

        SessionManager.Instance.stunButton.onClick.AddListener(OnStunBtnClick);
        DieEvent += () => { agent.SetVariableValue<bool>("isDead", true); };
        StunEvent += () => { agent.SetVariableValue<bool>("isStunned", true); };
        EnrageEvent += () => { agent.SetVariableValue<bool>("isEnraged", true); };
        ResetStatus();
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

    public void Attack(bool shouldSpAttack = false)
    {
        float damage = shouldSpAttack ? bossSpAtkDamage : bossAtkDamage;
        if (agent.GetVariable<bool>("isEnraged", out var shouldEnrage))
        {
            if (shouldEnrage)
            {
                damage *= enragedDamageBoost;
            }
        }

        AttackEvent?.Invoke();
        statusText.text = shouldSpAttack ? "Special Attack!" : "Attack!";
        SessionManager.Instance.UpdatePlayerHealth(damage);
    }

    public void OnStunBtnClick()
    {
        StunEvent?.Invoke();
    }

    public void ResetStatus()
    {
        if (agent.GetVariable<bool>("isEnraged", out var shouldEnrage))
        {
            statusText.text = shouldEnrage ? "Enraged!" : "Active!";
        }
    }

    public void EnrageCheck()
    {
        float healthPercentage = healthComponent.GetHealthPercentage();
        if (healthPercentage <= enragedHealthThreshold)
        {
            EnrageEvent?.Invoke();
            ResetStatus();
        }
    }

    public bool DeathCheck()
    {
        if (healthComponent.CurrentHealth <= 0)
        {
            DieEvent?.Invoke();
            return true;
        }
        return false;
    }

    public void Die()
    {
        statusText.text = "Defeated!";
    }

    public void Stun()
    {
        statusText.text = "Stunned!";
    }

    public float GetCooldown()
    {
        if (agent.GetVariable<bool>("isEnraged", out var shouldEnrage))
        {
            return shouldEnrage ? attackCoolDown * enragedCooldownReduction : attackCoolDown;
        }
        return attackCoolDown;
    }

    public float GetStunTimeSpan()
    {
        if (agent.GetVariable<bool>("isEnraged", out var shouldEnrage))
        {
            return shouldEnrage ? stunTimeSpan * enragedCooldownReduction : stunTimeSpan;
        }
        return stunTimeSpan;
    }
}
