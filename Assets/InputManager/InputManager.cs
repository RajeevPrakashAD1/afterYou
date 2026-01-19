using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerRecorder recorder;
    private CloneManager cloneManager;

    void Awake()
    {
        recorder = FindObjectOfType<PlayerRecorder>();
        cloneManager = FindObjectOfType<CloneManager>();
    }

    // Called automatically by PlayerInput
    void OnMove(InputValue value)
    {
        Vector2 move = value.Get<Vector2>();
        recorder.SetMove(move.x);
    }

    void OnJump()
    {
        recorder.PressJump();
    }

    void OnReset()
    {
        cloneManager.ResetRun();
    }
}