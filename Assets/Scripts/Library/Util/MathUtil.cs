using UnityEngine;

public static class MathUtils{
    public static Vector3 GetDirectionVectorFlat(Vector3 origin, Vector3 target){
        Vector3 direction = new(origin.x - target.x, 0, origin.z - target.z);
        return direction.normalized;
    }

    // TODO: Tweak, add an interesting equation, maybe?
    public static float CalculateDamage(float characterDamageStat, float weaponDamageStat){
        return characterDamageStat + weaponDamageStat;
    }
}
