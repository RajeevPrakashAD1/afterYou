using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerRecorder : MonoBehaviour
{
    private PlayerMotor motor;
    private readonly List<FrameInput> frames = new List<FrameInput>(1024);

    private float currentMove;
    private bool jumpPressed;

    void Awake()
    {
        motor = GetComponent<PlayerMotor>();
    }

    public void SetMove(float move)
    {
        currentMove = move;
        motor.SetMoveInput(move);
    }

    public void PressJump()
    {
        jumpPressed = true;
        motor.RequestJump();
    }

    void FixedUpdate()
    {
        frames.Add(new FrameInput
        {
            move = currentMove,
            jump = jumpPressed
        });

        jumpPressed = false;
    }

    public List<FrameInput> ConsumeRecording()
    {
        var copy = new List<FrameInput>(frames);
        frames.Clear();
        return copy;
    }
    public void ResetRecorder()
    {
        frames.Clear();
        currentMove = 0;
        jumpPressed = false;
    }

}