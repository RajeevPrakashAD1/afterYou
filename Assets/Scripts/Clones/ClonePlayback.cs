using System.Collections.Generic;
using UnityEngine;

public class ClonePlayback : MonoBehaviour
{
    private PlayerMotor motor;
    private List<FrameInput> recording;
    public float lifetime = -1f; // -1 = infinite
    private float aliveTime;

    public bool IsAlive => lifetime < 0 || aliveTime < lifetime;
    void Awake()
    {
        motor = GetComponent<PlayerMotor>();
    }

    public void SetRecording(List<FrameInput> data)
    {
        recording = data;
    }

    public void PlayFrame(int frameIndex)
    {
        if (recording == null || frameIndex >= recording.Count)
        {
            motor.SetMoveInput(0);
            return;
        }

        var frame = recording[frameIndex];
        motor.SetMoveInput(frame.move);

        if (frame.jump)
            motor.RequestJump();
    }

    public void ResetState()
    {
        
        motor.SetMoveInput(0);
    }
    
    public void Tick(float dt)
    {
        if (lifetime > 0)
            aliveTime += dt;
    }
}