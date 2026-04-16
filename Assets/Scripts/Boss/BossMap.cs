using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class BossMap : BossBase
{
    [SerializeField] private string bossName;
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
        GameManager.Instance.SetSelectedBoss(bossName);

        SceneManager.LoadScene("PrototypeScene");
    }
}
