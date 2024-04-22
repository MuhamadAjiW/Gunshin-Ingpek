using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// Util contains static functions
public static class ObjectFactory
{
    // Functions
    public static GameObject CreateObject(
        string prefabPath,
        Transform parent = null,
        Vector3? position = null,
        Vector3? scale = null,
        Quaternion? rotation = null,
        int renderingOrder = 0,
        string objectName = "Unnamed Object"
    )
    {
        GameObject prefabObject = Resources.Load<GameObject>(prefabPath);
        
        if(prefabObject == null)
        {
            Debug.LogError("Prefab not found: " + prefabPath);
        }

        return CreateObject(prefabObject, parent, position, scale, rotation, renderingOrder, objectName);
    }

    public static GameObject CreateObject(
        GameObject gameObject,
        Transform parent = null,
        Vector3? position = null,
        Vector3? scale = null,
        Quaternion? rotation = null,
        int renderingOrder = 0,
        string objectName = "Unnamed Object"
    )
    {
        GameObject createdObject = parent == null?
            GameObject.Instantiate(gameObject, ObjectManager.instance.transform) :
            GameObject.Instantiate(gameObject, parent);

        if(position != null)
        {
            createdObject.transform.position = position.Value;
        }
        if(rotation != null)
        {
            createdObject.transform.rotation *= rotation.Value;
        }
        if(scale != null)
        {
            createdObject.transform.localScale = Vector3.Scale(createdObject.transform.localScale, scale.Value);
        }
        if(createdObject.TryGetComponent<Renderer>(out var renderer))
        {
            renderer.sortingOrder = renderingOrder;
        }
        createdObject.name = objectName;

        return createdObject;
    }

    public static T CreateObject<T>(
        string prefabPath,
        Transform parent = null,
        Vector3? position = null,
        Vector3? scale = null,
        Quaternion? rotation = null,
        int renderingOrder = 0,
        string objectName = "Unnamed Object"
    ) where T : MonoBehaviour 
    {
        GameObject prefabObject = CreateObject(
            prefabPath, 
            parent == null? ObjectManager.instance.transform : parent, 
            position, 
            scale, 
            rotation, 
            renderingOrder, 
            objectName
        );

        if(!prefabObject.TryGetComponent<T>(out var UnityObject))
        {
            Debug.LogError("Loaded prefab is not a a valid type: " + prefabPath);
        }

        return UnityObject;
    }

    public static T CreateObject<T>(
        GameObject gameObject,
        Transform parent = null,
        Vector3? position = null,
        Vector3? scale = null,
        Quaternion? rotation = null,
        int renderingOrder = 0,
        string objectName = "Unnamed Object"
    ) where T : MonoBehaviour 
    {
        GameObject prefabObject = CreateObject(
            gameObject, 
            parent == null? ObjectManager.instance.transform : parent, 
            position, 
            scale, 
            rotation, 
            renderingOrder, 
            objectName
        );

        if(!prefabObject.TryGetComponent<T>(out var UnityObject))
        {
            Debug.LogError("Loaded gameobject is not a a valid type: " + gameObject.name);
        }

        return UnityObject;
    }

    public static AttackObject CreateAttackObject(
        string prefabPath,
        float damage,
        float knockbackPower,
        Vector3 knockbackOrigin,
        string attackLayerCode = EnvironmentConfig.LAYER_ENVIRONMENT_ATTACK,
        float damageModifier = 0,
        Transform parent = null,
        Vector3? position = null,
        Vector3? scale = null,
        Quaternion? rotation = null,
        int renderingOrder = 0,
        string objectName = "Unnamed Object"
    )
    {
        AttackObject attackObject = CreateObject<AttackObject>(
            prefabPath, 
            parent == null? ObjectManager.instance.transform : parent, 
            position, 
            scale, 
            rotation, 
            renderingOrder, 
            objectName
        );

        attackObject.Damage = damage;
        attackObject.KnockbackPower = knockbackPower;
        attackObject.KnockbackOrigin = knockbackOrigin;
        attackObject.gameObject.layer = 
        attackObject.gameObject.layer = LayerMask.NameToLayer(attackLayerCode);
        attackObject.Damage *= damageModifier;

        return attackObject;
    }

    public static T CreateAttackObject<T>(
        string prefabPath,
        float damage,
        float knockbackPower,
        Vector3 knockbackOrigin,
        string attackLayerCode = EnvironmentConfig.LAYER_ENVIRONMENT_ATTACK,
        float damageModifier = 0,
        Transform parent = null,
        Vector3? position = null,
        Vector3? scale = null,
        Quaternion? rotation = null,
        int renderingOrder = 0,
        string objectName = "Unnamed Object"
    ) where T : AttackObject
    {
        AttackObject attackObject = CreateAttackObject(
            prefabPath, 
            damage, 
            knockbackPower, 
            knockbackOrigin, 
            attackLayerCode, 
            damageModifier,
            parent, 
            position, 
            scale, 
            rotation, 
            renderingOrder, 
            objectName
        );

        if(!attackObject.TryGetComponent<T>(out var UnityObject))
        {
            Debug.LogError("Loaded prefab is not a a valid type: " + prefabPath);
        }

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
        Collectible collectible = CreateObject<Collectible>(
            prefabPath, 
            ObjectManager.instance.transform, 
            position, 
            scale, 
            rotation, 
            renderingOrder, 
            objectName
        );

        collectible.gameObject.layer = LayerMask.NameToLayer(EnvironmentConfig.LAYER_COLLECTIBLE);
        
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
        WorldEntity prefabObject = CreateObject<WorldEntity>(
            prefabPath, 
            EntityManager.instance.transform, 
            position, 
            scale, 
            rotation, 
            renderingOrder, 
            objectName
        );

        return prefabObject;
    }

    public static void DestroyObject(MonoBehaviour gameObject, float delay = 0)
    {
        if(gameObject == null) return;
        GameController.instance.StartCoroutine(DestroyWithDelay(gameObject.gameObject, delay));
    }

    // Internal functions
    private static IEnumerator DestroyWithDelay(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject.Destroy(gameObject);
    }
}
