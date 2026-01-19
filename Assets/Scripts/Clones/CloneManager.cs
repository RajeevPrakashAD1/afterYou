using System.Collections.Generic;
using UnityEngine;

public class CloneManager : MonoBehaviour
{
    [SerializeField] GameObject clonePrefab;
    [SerializeField] Transform playerSpawn;
    [SerializeField] GameObject player;
    [SerializeField] int maxClones = 20;
    [SerializeField] private bool allowFullCollision = false;

    private PlayerRecorder recorder;
    private List<List<FrameInput>> recordings = new();
    private List<ClonePlayback> clones = new();

    private int globalFrame;

    void Awake()
    {
        recorder = player.GetComponent<PlayerRecorder>();
    }

    public void ResetRun()
    {
        // Save current run
        var run = recorder.ConsumeRecording();
        if (run.Count > 0)
        {
            recordings.Add(run);

            var cloneObj = Instantiate(clonePrefab, playerSpawn.position, Quaternion.identity);
            var controller = cloneObj.GetComponent<CloneCollisionController>();

            controller.SetMode(
                allowFullCollision
                    ? CloneCollisionMode.FullBody
                    : CloneCollisionMode.TopOnly
            );

            var playback = cloneObj.GetComponent<ClonePlayback>();
            playback.SetRecording(run);
            clones.Add(playback);
        }

        // Reset EVERYTHING
        globalFrame = 0;
        recorder.ResetRecorder();

        player.transform.position = playerSpawn.position;

        foreach (var c in clones)
        {
            c.transform.position = playerSpawn.position;
            c.ResetState();
        }
        
        if (clones.Count >= maxClones)
        {
            Destroy(clones[0].gameObject);
            clones.RemoveAt(0);
            recordings.RemoveAt(0);
        }

    }

    void FixedUpdate()
    {
        globalFrame++;

        for (int i = clones.Count - 1; i >= 0; i--)
        {
            var clone = clones[i];

            clone.PlayFrame(globalFrame);
            clone.Tick(Time.fixedDeltaTime);

            if (!clone.IsAlive)
            {
                Destroy(clone.gameObject);
                clones.RemoveAt(i);
            }
        }
    }

}