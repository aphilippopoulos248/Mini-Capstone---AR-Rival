using Immersal.XR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Behavior;
using TMPro;
using UnityEngine.UI;

public class EnemyController : BossBase
{
    private HealthComponent healthComponent;
    private Animator animator;

    [SerializeField] private float tapDamage = 10f;
    [SerializeField] private float enemySwipeDamage = 10f;
    [SerializeField] private float enemyFlameDamage = 5f;
    public float attackCoolDown = 2f;
    public float stunTimeSpan = 3f;

    public bool isAttacking = false;
    [SerializeField] private float enragedHealthThreshold = 0.5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        healthComponent = GetComponent<HealthComponent>();

        FSMBaseState[] behaviours = animator.GetBehaviours<FSMBaseState>();
        foreach (var behaviour in behaviours)
        {
            behaviour.Init(this, animator);
        }

        SessionManager.Instance.stunButton.onClick.AddListener(OnStunBtnClick);
        DieEvent += () => { animator.SetTrigger("Dead"); };
        StunEvent += () => { animator.SetBool("Stunned", true); };
        EnrageEvent += () => { animator.SetBool("Enraged", true); };
    }

    private void Update()
    {
        if (!animator.GetBool("Stunned") && !animator.GetBool("Dead"))
        {
            if (!isAttacking)
            {
                animator.SetTrigger("Attack");
            }
        }
    }

    private void OnEnable()
    {
        actions.Default.Attack.performed += OnTouch;
        actions.Enable();
    }

    private void OnDisable()
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
        Debug.Log("Enemy hit!");
        healthComponent.TakeDamage(tapDamage);
        healthComponent.ShowDamageNumber(tapDamage, Color.red);

        if (!DeathCheck()) EnrageCheck();

        Debug.Log("Enemy Current Health: " + healthComponent.CurrentHealth);
    }

    public void EnrageCheck()
    {
        float healthPercentage = healthComponent.GetHealthPercentage();
        if (healthPercentage <= enragedHealthThreshold)
        {
            EnrageEvent?.Invoke();
        }
    }

    public bool DeathCheck()
    {
        if (healthComponent.CurrentHealth <= 0)
        {
            ResetStatus();
            DieEvent?.Invoke();
            return true;
        }
        return false;
    }

    public void ResetStatus()
    {
        animator.SetBool("Stunned", false);
        animator.ResetTrigger("Attack");
    }

    public void OnStunBtnClick()
    {
        ResetStatus();
        StunEvent?.Invoke();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
