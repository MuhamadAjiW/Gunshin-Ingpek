using UnityEngine;

public static class ObjectDataManager
{
    // Functions
    public static ScriptableObject GetObjectData(
        string dataPath
    )
    {
        ScriptableObject dataObject = Resources.Load<ScriptableObject>(dataPath);
        
        #if STRICT
        if(dataObject == null)
        {
            Debug.LogError($"Prefab not found: {dataPath}. How to resolve: check the prefabPath parameter, make sure a prefab on path Resources/{dataPath} actually exist");
        }
        #endif

        return dataObject;
    }

    public static T GetObjectData<T>(
        string dataPath
    ) where T : ScriptableObject 
    {
        ScriptableObject dataObject = GetObjectData(dataPath);
        return dataObject as T;
    }
}