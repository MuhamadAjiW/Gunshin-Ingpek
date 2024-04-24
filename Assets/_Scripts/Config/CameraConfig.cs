
using UnityEngine;

// Configs should only contain constants and static readonly classes
public static class CameraConfig
{
    // Movement lerp constants
    public const float DEFAULT_FOLLOWING_SPEED = 0.1f;
    // Offset for follow camera
    public static readonly Vector3 DEFAULT_CAMERA_OFFSET = new(0f, 0.2f, -2f);

    public static readonly float MAX_AIM_ANGLE = 50.0f;
}