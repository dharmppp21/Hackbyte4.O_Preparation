using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class TwoPlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        // Check which control scheme this specific player was spawned with
        if (playerInput.currentControlScheme == "KeyboardLeft")
        {
            // Player 1 ONLY reads from WASD
            moveInput = playerInput.actions["Move_P1"].ReadValue<Vector2>();
        }
        else if (playerInput.currentControlScheme == "KeyboardRight")
        {
            // Player 2 ONLY reads from the Arrows
            moveInput = playerInput.actions["Move_P2"].ReadValue<Vector2>();
        }

        // Apply movement
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}