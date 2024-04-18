using UnityEngine;

public static class MathUtils{
    public static Vector3 GetDirectionVector(Vector3 origin, Vector3 target){
        Vector3 direction = origin - target;
        return direction.normalized;
    }

    // TODO: Tweak, add an interesting equation, maybe?
    public static float CalculateDamage(float characterDamageStat, float weaponDamageStat){
        return characterDamageStat + weaponDamageStat;
    }
}
