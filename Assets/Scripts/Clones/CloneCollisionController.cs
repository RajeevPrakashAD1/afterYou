using UnityEngine;


public enum CloneCollisionMode
{
    TopOnly,
    FullBody
}

public class CloneCollisionController : MonoBehaviour
{
    [SerializeField] BoxCollider2D bodyCollider;
    [SerializeField] BoxCollider2D topPlatform;

    public void SetMode(CloneCollisionMode mode)
    {
        switch (mode)
        {
            case CloneCollisionMode.TopOnly:
                bodyCollider.enabled = false;
                topPlatform.enabled = true;
                break;

            case CloneCollisionMode.FullBody:
                bodyCollider.enabled = true;
                topPlatform.enabled = false;
                break;
        }
    }
}