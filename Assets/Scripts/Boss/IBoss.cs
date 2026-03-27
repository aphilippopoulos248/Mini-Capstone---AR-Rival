using UnityEngine;
using UnityEngine.InputSystem;

public interface IBoss
{
    public void OnTouch(InputAction.CallbackContext ctx);
    public void OnRaycastHit(RaycastHit hit);
}
