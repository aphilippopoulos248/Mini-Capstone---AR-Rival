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
        Vector2 screenPosition = Pointer.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            healthComponent.TakeDamage(tapDamage);
            Debug.Log("Boss hit!");
            
        }
    }
}
