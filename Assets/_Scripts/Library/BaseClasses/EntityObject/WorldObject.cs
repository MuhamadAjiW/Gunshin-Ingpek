using UnityEngine;

public class WorldObject : MonoBehaviour
{
    // Attributes
    public string id;
    private static int autoIncrement = 0;
    private int numberId; 
    private string prefix;
    private string layerCode = EnvironmentConfig.LAYER_DEFAULT;

    // Set-Getters
    public Vector3 Front => transform.rotation * Vector3.forward;
    public Quaternion Rotation => transform.rotation;
    public string LayerCode
    {
        get => layerCode;
        set => layerCode = value;
    }

    // Constructor
    protected void Start()
    {
        #if STRICT
        if(GameController.Instance == null
         || GameSaveData.Instance == null
         || GameInput.Instance == null
         || EnvironmentManager.Instance == null
         )
        {
            Debug.LogError("The structural controller scripts does not exist in the scene. How to resolve: refer to https://docs.google.com/document/d/14ypRPRArb10h4RO5I6qJBkxmQ-dMVWQy3zJeMVDpSsM/edit#heading=h.rucy43z24dch for the scene's base structure");
        }
        #endif

        numberId = autoIncrement;
        id = numberId.ToString();
        autoIncrement++;

        gameObject.layer = LayerMask.NameToLayer(LayerCode);
    }

    public void SetLayer(string layerCode)
    {
        LayerCode = layerCode;
        gameObject.layer = LayerMask.NameToLayer(LayerCode);
    }


    // Functions
    protected void SetIdPrefix(string prefix)
    {
        this.prefix = prefix;
        if(this.prefix == "")
        {
            id = numberId.ToString();
        } 
        else
        {
            id = this.prefix + "_" + id;
        }
    }
}
