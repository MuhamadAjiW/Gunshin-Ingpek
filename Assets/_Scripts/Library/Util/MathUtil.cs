using UnityEngine;

// Util contains static functions
public static class MathUtil
{
    public static Vector3 GetDirectionVectorFlat(Vector3 target, Vector3 origin)
    {
        Vector3 direction = new(target.x - origin.x, 0, target.z - origin.z);
        return direction.normalized;
    }

    public static Vector3 GetDirectionVectorClamped(Vector3 target, Vector3 origin, float clampDegrees)
    {
        Vector3 direction = target - origin;
        // Vector3 flatDirection = new(target.x - origin.x, 0, target.z - origin.z);
        // float angle = Vector3.Angle(flatDirection, direction);
        // float clamped = Mathf.Clamp(angle, -clampDegrees, clampDegrees);

        // Quaternion rotation = Quaternion.AngleAxis(clamped, Vector3.right);
        // Vector3 finalDirection = rotation * flatDirection;

        // return finalDirection.normalized;
        return direction.normalized;
    }

    // TODO: Tweak, add an interesting equation, maybe?
    public static float CalculateDamage(float characterDamageStat, float weaponDamageStat)
    {
        return characterDamageStat + weaponDamageStat;
    }
}
