using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        Debug.Log(bossName);

        SceneManager.LoadScene("PrototypeScene");
    }
}
