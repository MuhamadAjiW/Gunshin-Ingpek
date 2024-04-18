using UnityEngine;

public static class MathUtils{
    public static Vector3 GetDirectionVector(Vector3 origin, Vector3 target){
        Vector3 direction = origin - target;
        return direction.normalized;
    }
}