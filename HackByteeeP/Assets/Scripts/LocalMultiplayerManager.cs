using UnityEngine;
using UnityEngine.InputSystem;

public class LocalMultiplayerManager : MonoBehaviour
{
    [Header("Player Setup")]
    [Tooltip("The prefab must have a PlayerInput component attached.")]
    public GameObject playerPrefab;

    [Header("Spawn Points")]
    public Transform player1Spawn;
    public Transform player2Spawn;

    // State tracking
    private bool player1Joined = false;
    private bool player2Joined = false;

    void Update()
    {
        // Safety check: Ensure a keyboard is actually connected
        if (Keyboard.current == null) return;

        // --- PLAYER 1 JOIN LOGIC (Space Key) ---
        if (!player1Joined && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            JoinPlayer(0, "KeyboardLeft", player1Spawn.position);
            player1Joined = true;
            //Debug.Log("Player 1 Joined! (WASD)");
        }

        // --- PLAYER 2 JOIN LOGIC (Enter Key) ---
        // Note: numpadEnterKey and enterKey are separate in the New Input System
        if (!player2Joined && (Keyboard.current.enterKey.wasPressedThisFrame || Keyboard.current.numpadEnterKey.wasPressedThisFrame))
        {
            JoinPlayer(1, "KeyboardRight", player2Spawn.position);
            player2Joined = true;
            //Debug.Log("Player 2 Joined! (Arrows)");
        }
    }

    private void JoinPlayer(int playerIndex, string schemeName, Vector3 spawnPosition)
    {
        // This is the core API call that makes device sharing work.
        // It spawns the prefab, assigns the specific scheme, and pairs it to the shared keyboard.
        PlayerInput newPlayer = PlayerInput.Instantiate(
            playerPrefab,
            playerIndex: playerIndex,
            controlScheme: schemeName,
            pairWithDevice: Keyboard.current
        );

        // Move the newly spawned player to their starting position
        newPlayer.transform.position = spawnPosition;
    }
}