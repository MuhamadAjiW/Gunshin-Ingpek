using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class Dialog : VisualElement
{
    public static readonly string CLASSNAME_PERSON_IMAGE = "dialog-person-image";
    public static readonly string CLASSNAME_PERSON_R_LABEL_CONTAINER = "dialog-person-r-label";
    public static readonly string CLASSNAME_PERSON_L_LABEL_CONTAINER = "dialog-person-l-label";
    public static readonly string CLASSNAME_OVERLAY = "dialog-overlay";
    public static readonly string CLASSNAME_AREA = "dialog-area";
    public static readonly string CLASSNAME_PERSON_AREA = "dialog-person-area";
    public static readonly string CLASSNAME_PERSON_GAP = "dialog-person-gap";
    public static readonly string CLASSNAME_MAIN_CONTAINER = "dialog-main-container";
    public static readonly string CLASSNAME_PERSON_L_LABEL_TEXT = "dialog-person-l-label-text";
    public static readonly string CLASSNAME_PERSON_R_LABEL_TEXT = "dialog-person-r-label-text";
    public static readonly string CLASSNAME_MAIN_TEXT = "dialog-main-text";

    public VisualElement m_Overlay;
    public VisualElement m_Area;
    public VisualElement m_PersonArea;
    public VisualElement m_PersonLImage;
    public VisualElement m_PersonLLabelContainer;
    public Label m_PersonLLabelText;
    public VisualElement m_PersonGap;
    public VisualElement m_PersonRImage;
    public VisualElement m_PersonRLabelContainer;
    public Label m_PersonRLabelText;
    public VisualElement m_MainTextContainer;
    public Label m_MainText;

    // [SerializeField, DontCreateProperty] string attr_Text;
    // [SerializeField, DontCreateProperty] Color attr_OverlayColor;
    // [SerializeField, DontCreateProperty] bool attr_PersonLActive;
    // [SerializeField, DontCreateProperty] string attr_PersonLName;
    // [SerializeField, DontCreateProperty] StyleBackground attr_PersonLImage;
    // [SerializeField, DontCreateProperty] bool attr_PersonRActive;
    // [SerializeField, DontCreateProperty] string attr_PersonRName;
    // [SerializeField, DontCreateProperty] StyleBackground attr_PersonRImage;


    // [UxmlAttribute, CreateProperty]
    // public string Text
    // {
    //     get => attr_Text;
    //     set
    //     {
    //         attr_Text = value;
    //         MarkDirtyRepaint();
    //     }
    // }

    // [UxmlAttribute, CreateProperty]
    // public Color OverlayColor
    // {
    //     get => attr_OverlayColor;
    //     set
    //     {
    //         attr_OverlayColor = value;
    //         MarkDirtyRepaint();
    //     }
    // }

    // [UxmlAttribute, CreateProperty]
    // public bool PersonLActive
    // {
    //     get => attr_PersonLActive;
    //     set
    //     {
    //         attr_PersonLActive = value;
    //         MarkDirtyRepaint();
    //     }
    // }
    
    // [UxmlAttribute, CreateProperty]
    // public bool PersonRActive
    // {
    //     get => attr_PersonRActive;
    //     set
    //     {
    //         attr_PersonRActive = value;
    //         MarkDirtyRepaint();
    //     }
    // }

    // [UxmlAttribute, CreateProperty]
    // public string PersonRLabel
    // {
    //     get => attr_PersonRName;
    //     set
    //     {
    //         attr_PersonRName = value;
    //         MarkDirtyRepaint();
    //     }
    // }
    
    // [UxmlAttribute, CreateProperty]
    // public StyleBackground PersonRImage
    // {
    //     get => attr_PersonRImage;
    //     set
    //     {
    //         attr_PersonRImage = value;
    //         MarkDirtyRepaint();
    //     }
    // }
    
    // [UxmlAttribute, CreateProperty]
    // public string PersonLLabel
    // {
    //     get => attr_PersonLName;
    //     set
    //     {
    //         attr_PersonLName = value;
    //         MarkDirtyRepaint();
    //     }
    // }
    
    // [UxmlAttribute, CreateProperty]
    // public StyleBackground PersonLImage
    // {
    //     get => attr_PersonLImage;
    //     set
    //     {
    //         attr_PersonLImage = value;
    //         MarkDirtyRepaint();
    //     }
    // }

    public Dialog()
    {
        // Debug.Log(childCount);
        // Get references of the elements, hardcoded, I'm sorry
        m_Overlay = new VisualElement
        {
            name = "DialogOverlay"
        };
        m_Overlay.AddToClassList(CLASSNAME_OVERLAY);
        
        m_Area = new VisualElement
        {
            name = "DialogArea"
        };
        m_Area.AddToClassList(CLASSNAME_AREA);
        
        m_PersonArea = new VisualElement
        {
            name = "DialogPersonArea"
        };
        m_PersonArea.AddToClassList(CLASSNAME_PERSON_AREA);
        
        m_PersonLImage = new VisualElement
        {
            name = "PersonLImage"
        };
        m_PersonLImage.AddToClassList(CLASSNAME_PERSON_IMAGE);
        
        m_PersonLLabelContainer = new VisualElement
        {
            name = "PersonLLabelContainer"
        };
        m_PersonLLabelContainer.AddToClassList(CLASSNAME_PERSON_L_LABEL_CONTAINER);
        
        m_PersonLLabelText = new Label
        {
            name = "PersonLLabelText"
        };
        m_PersonLLabelText.AddToClassList(CLASSNAME_PERSON_L_LABEL_TEXT);
        
        m_PersonGap = new VisualElement
        {
            name = "PersonGap"
        };
        m_PersonGap.AddToClassList(CLASSNAME_PERSON_GAP);
        
        m_PersonRImage = new VisualElement
        {
            name = "PersonRImage"
        };
        m_PersonRImage.AddToClassList(CLASSNAME_PERSON_IMAGE);

        m_PersonRLabelContainer = new VisualElement
        {
            name = "PersonRLabelContainer"
        };
        m_PersonRLabelContainer.AddToClassList(CLASSNAME_PERSON_R_LABEL_CONTAINER);
        
        m_PersonRLabelText = new Label
        {
            name = "PersonRLabelText"
        };
        m_PersonRLabelText.AddToClassList(CLASSNAME_PERSON_R_LABEL_TEXT);
        
        m_MainTextContainer = new VisualElement
        {
            name = "MainTextContainer"
        };
        m_MainTextContainer.AddToClassList(CLASSNAME_MAIN_CONTAINER);
        
        m_MainText = new Label
        {
            name = "MainText"
        };
        m_MainText.AddToClassList(CLASSNAME_MAIN_TEXT);
        
        Add(m_Overlay);
        
        m_Overlay.Add(m_Area);

        m_Area.Add(m_PersonArea);
        m_Area.Add(m_MainTextContainer);

        m_PersonArea.Add(m_PersonLImage);
        m_PersonArea.Add(m_PersonLLabelContainer);
        m_PersonArea.Add(m_PersonGap);
        m_PersonArea.Add(m_PersonRLabelContainer);
        m_PersonArea.Add(m_PersonRImage);

        m_PersonLLabelContainer.Add(m_PersonLLabelText);
        m_PersonRLabelContainer.Add(m_PersonRLabelText);

        m_MainTextContainer.Add(m_MainText);

        // generateVisualContent += GenerateVisualContext;
    }

    // void GenerateVisualContext(MeshGenerationContext context)
    // {
    //     m_Overlay.style.backgroundColor = attr_OverlayColor;
    //     m_PersonLLabelContainer.visible = attr_PersonLActive;
    //     m_PersonLImage.visible = attr_PersonLActive;
    //     m_PersonLImage.style.backgroundImage = attr_PersonLImage;

    //     m_PersonLLabelText.text = attr_PersonLName;

    //     m_PersonRLabelContainer.visible = attr_PersonRActive;
    //     m_PersonRImage.visible = attr_PersonRActive;
    //     m_PersonRImage.style.backgroundImage = attr_PersonRImage;
    //     m_PersonRLabelText.text = attr_PersonRName;
        
    //     m_MainText.text = attr_Text;
    // }
}