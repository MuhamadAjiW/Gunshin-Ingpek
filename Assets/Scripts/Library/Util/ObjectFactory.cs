using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public static class ObjectFactory{
    // Internal functions
    private static GameObject CreateObject(
        GameObject gameObject,
        Transform parent = null,
        Vector3? position = null,
        Vector3? scale = null,
        Quaternion? rotation = null,
        int renderingOrder = 0,
        string objectName = "Unnamed Object"
    ){
        GameObject returnObject = parent == null? GameObject.Instantiate(gameObject, ObjectManager.instance.transform) : GameObject.Instantiate(gameObject, parent);
        if(position != null) returnObject.transform.position = position.Value;
        if(rotation != null) returnObject.transform.rotation *= rotation.Value;
        if(scale != null) returnObject.transform.localScale = Vector3.Scale(returnObject.transform.localScale, scale.Value);
        if(returnObject.TryGetComponent<Renderer>(out var renderer)) renderer.sortingOrder = renderingOrder;
        returnObject.name = objectName;

        return returnObject;
    }

    // External functions
    public static GameObject CreateObject(
        string prefabPath,
        Transform parent = null,
        Vector3? position = null,
        Vector3? scale = null,
        Quaternion? rotation = null,
        int renderingOrder = 0,
        string objectName = "Unnamed Object"
    ){
        GameObject prefabObject = Resources.Load<GameObject>(prefabPath);
        if(prefabObject == null) Debug.LogError("Prefab not found: " + prefabPath);
        return CreateObject(prefabObject, parent, position, scale, rotation, renderingOrder, objectName);
    }

    public static T CreateObject<T>(
        string prefabPath,
        Transform parent = null,
        Vector3? position = null,
        Vector3? scale = null,
        Quaternion? rotation = null,
        int renderingOrder = 0,
        string objectName = "Unnamed Object"
    ) where T : MonoBehaviour {
        GameObject prefabObject = CreateObject(prefabPath, parent == null? ObjectManager.instance.transform : parent, position, scale, rotation, renderingOrder, objectName);
        if(!prefabObject.TryGetComponent<T>(out var UnityObject)) Debug.LogError("Loaded prefab is not a a valid type: " + prefabPath);
        return UnityObject;
    }

    public static AttackObject CreateAttackObject(
        string prefabPath,
        float damage,
        float knockbackPower,
        Vector3 knockbackOrigin,
        AttackObjectType type,
        Transform parent = null,
        Vector3? position = null,
        Vector3? scale = null,
        Quaternion? rotation = null,
        int renderingOrder = 0,
        string objectName = "Unnamed Object"
    ){
        AttackObject attackObject = CreateObject<AttackObject>(prefabPath, parent == null? ObjectManager.instance.transform : parent, position, scale, rotation, renderingOrder, objectName);

        attackObject.Damage = damage;
        attackObject.KnockbackPower = knockbackPower;
        attackObject.KnockbackOrigin = knockbackOrigin;

        switch (type){
            case AttackObjectType.PLAYER:
                attackObject.gameObject.layer = LayerMask.NameToLayer(GameEnvironmentConfig.LAYER_PLAYER_ATTACK);
                attackObject.Damage *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.instance.difficulty].PlayerDamageMultiplier;
                break;
            case AttackObjectType.ENEMY:
                attackObject.gameObject.layer = LayerMask.NameToLayer(GameEnvironmentConfig.LAYER_ENEMY_ATTACK);
                attackObject.Damage *= GameConfig.DIFFICULTY_MODIFIERS[GameSaveData.instance.difficulty].EnemyDamageMultiplier;
                break;
            case AttackObjectType.ENVIRONMENT:
                attackObject.gameObject.layer = LayerMask.NameToLayer(GameEnvironmentConfig.LAYER_ENVIRONMENT_ATTACK);
                break;
            default:
                Debug.LogError("Invalid AttackObjectType set, please refer to enum AttackObjectType for valid types");
                break;
        }

        return attackObject;
    }

    public static T CreateAttackObject<T>(
        string prefabPath,
        float damage,
        float knockbackPower,
        Vector3 knockbackOrigin,
        AttackObjectType type,
        Transform parent = null,
        Vector3? position = null,
        Vector3? scale = null,
        Quaternion? rotation = null,
        int renderingOrder = 0,
        string objectName = "Unnamed Object"
    ) where T : AttackObject {
        AttackObject attackObject = CreateAttackObject(prefabPath, damage, knockbackPower, knockbackOrigin, type, parent, position, scale, rotation, renderingOrder, objectName);
        if(!attackObject.TryGetComponent<T>(out var UnityObject)) Debug.LogError("Loaded prefab is not a a valid type: " + prefabPath);
        return UnityObject;
    }


    public static Collectible CreateCollectibleObject(
        string prefabPath,
        Vector3? position = null,
        Vector3? scale = null,
        Quaternion? rotation = null,
        int renderingOrder = 0,
        string objectName = "Unnamed Object"
    ){
        Collectible collectible = CreateObject<Collectible>(prefabPath, ObjectManager.instance.transform, position, scale, rotation, renderingOrder, objectName);
        collectible.gameObject.layer = LayerMask.NameToLayer(GameEnvironmentConfig.LAYER_COLLECTIBLE);
        return collectible;
    }

    public static WorldEntity CreateEntity(
        string prefabPath,
        Vector3? position = null,
        Vector3? scale = null,
        Quaternion? rotation = null,
        int renderingOrder = 0,
        string objectName = "Unnamed Object"
    ){
        WorldEntity prefabObject = CreateObject<WorldEntity>(prefabPath, EntityManager.instance.transform, position, scale, rotation, renderingOrder, objectName);
        return prefabObject;
    }

    public static void DestroyObject(MonoBehaviour gameObject, float delay = 0){
        if(gameObject == null) return;
        GameController.instance.StartCoroutine(DestroyWithDelay(gameObject.gameObject, delay));
    }

    private static IEnumerator DestroyWithDelay(GameObject gameObject, float delay){
        yield return new WaitForSeconds(delay);
        GameObject.Destroy(gameObject);
    }
}
