
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossBase : MonoBehaviour, IBoss
{
    protected PlayerActions actions;
    public System.Action DieEvent;
    public System.Action StunEvent;
    public System.Action SpAttackEvent;
    public System.Action AttackEvent;
    public System.Action EnrageEvent;

    private static int lastFrameProcessed = -1;

    void Awake()
    {
        actions = new PlayerActions();
    }

    public virtual void OnTouch(InputAction.CallbackContext ctx)
    {
        if (lastFrameProcessed == Time.frameCount) return;
        lastFrameProcessed = Time.frameCount;

        Vector2 screenPosition = Pointer.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<IBoss>(out IBoss boss))
            {
                boss.OnRaycastHit(hit);
            }
        }
    }

    public virtual void OnRaycastHit(RaycastHit hit)
    {

    }
}

