using UnityEngine;
using UnityEngine.InputSystem;

public class Boss : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private float tapDamage = 10f;

    private PlayerActions actions;

    void Awake()
    {
        actions = new PlayerActions();
    }

    private void Start()
    {
        //healthComponent.TakeDamage(tapDamage);
        //healthComponent.ShowDamageNumber(tapDamage, Color.red);
        //Debug.Log("Boss Current Health: " + healthComponent.CurrentHealth);
    }

    void OnEnable()
    {
        actions.Default.Attack.performed += OnAttack;
        actions.Enable();
    }

    void OnDisable()
    {
        actions.Default.Attack.performed -= OnAttack;
        actions.Disable();
    }

    void OnAttack(InputAction.CallbackContext ctx)
    {
        Debug.Log("Attacking!");
        
        //healthComponent.TakeDamage(tapDamage);
        //healthComponent.ShowDamageNumber(tapDamage, Color.red);
        //Debug.Log("Boss Current Health: " + healthComponent.CurrentHealth);

        Vector2 screenPosition = Pointer.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            healthComponent.TakeDamage(tapDamage);
            healthComponent.ShowDamageNumber(tapDamage, Color.red);
            Debug.Log("Boss Current Health: " + healthComponent.CurrentHealth);

        }
    }
}
