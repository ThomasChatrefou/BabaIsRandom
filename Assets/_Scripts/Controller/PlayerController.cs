using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IMoveAction
{
    public event Action<Vector2Int> OnMove;
}

public class PlayerController : WorldBehaviour, IMoveAction
{
    public event Action<Vector2Int> OnMove;

    public bool IsMultiSelecting => _controls.Player.MultiSelection.ReadValue<float>() > 0f;

    #region Private

    private void Awake()
    {
        World.Register(this);
        _controls = new InputMaster();
        _controls.Player.Movement.performed += OnPlayerMovementPerformed;
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    private void OnPlayerMovementPerformed(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        // could be externalized in another implem of IMoveAction ?
        {
            Vector2Int gridMove = Vector2Int.zero;
            if (direction.x >= 0 && direction.y < direction.x && direction.y > -direction.x) gridMove = Vector2Int.right;
            else if (direction.x <= 0 && direction.y < -direction.x && direction.y > direction.x) gridMove = Vector2Int.left;
            else if (direction.y >= 0 && direction.y > direction.x && direction.y > -direction.x) gridMove = Vector2Int.up;
            else if (direction.y <= 0 && direction.y < direction.x && direction.y < -direction.x) gridMove = Vector2Int.down;
            OnMove?.Invoke(gridMove);
        }
    }

    [NonSerialized]
    private InputMaster _controls;
    
    #endregion Private
}
