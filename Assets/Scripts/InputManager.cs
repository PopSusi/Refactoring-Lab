using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public delegate void Button();
    public static Button Shoot;
    public static Button Restart;

    public delegate void Vec2(Vector2 pass);
    public static Vec2 OnMove; 

    public InputActionAsset actions;
    // Start is called before the first frame update
    void Awake()
    {
        actions.Enable();
        actions.FindAction("Shoot").started += ShootInvoke;
        actions.FindAction("Restart").performed += ResetInvoke;
        actions.FindAction("Restart").canceled += ResetInvoke;
    }

    void Update()
    {
        Vector2 tempMoveInput = actions.FindAction("Move").ReadValue<Vector2>();
        if (tempMoveInput.x != 0 || tempMoveInput.y != 0)
        {
            MoveInvoke(tempMoveInput);
        }
    }

    void ShootInvoke(InputAction.CallbackContext context)
    {
        if (Shoot != null) Shoot();
    }
    void MoveInvoke(Vector2 passInput)
    {
        if (OnMove != null) OnMove(passInput);
    }
    void ResetInvoke(InputAction.CallbackContext context)
    {
        Restart();
    }
}
