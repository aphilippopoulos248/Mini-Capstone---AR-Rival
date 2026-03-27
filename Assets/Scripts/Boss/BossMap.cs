using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BossMap : BossBase
{
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
        SceneManager.LoadScene("PrototypeScene");
    }
}
