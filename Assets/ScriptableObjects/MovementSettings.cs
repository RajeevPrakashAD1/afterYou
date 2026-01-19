using UnityEngine;

[CreateAssetMenu(
    fileName = "MovementSettings",
    menuName = "Game/Movement Settings"
)]
public class MovementSettings : ScriptableObject
{
    [Header("Horizontal")]
    public float moveSpeed = 6f;

    [Header("Jump")]
    public float jumpHeight = 3.8f;

    [Header("Gravity")]
    public float gravityScale = 3f;
}