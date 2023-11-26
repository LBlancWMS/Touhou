using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private character c;
    void Awake()
    {  
        c = GameObject.FindGameObjectWithTag("player").GetComponent<character>();
    }

    void OnEnable()
    {
    }

    void OnDisable()
    {
    }

    public void joystickMove(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            
            Debug.Log("started");
        }
        else if (ctx.performed)
        {
            c.startMove(ctx.ReadValue<Vector2>());
            Debug.Log("performed");
        }
        else if (ctx.canceled)
        {
            c.stopMove();
        }
    }

    public void Rotate(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {

        }
        else if(ctx.performed)
        {

        }
        else if(ctx.canceled)
        {

        }
    }

    public void Shoot(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {

        }
        else if(ctx.performed)
        {
            c.startShoot(ctx.ReadValue<Vector2>());
        }
        else if(ctx.canceled)
        {
            c.stopShoot();
        }
    }
}
