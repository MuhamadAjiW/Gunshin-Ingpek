using System;
using System.Collections.Generic;

// Configs should only contain constants and static readonly classes
public static class StoryConfig
{
    // Level 1
    public const string KEY_STORY_1_START_CUTSCENE = "EVENT_STORY1_1";
    public const string KEY_STORY_1_ENTER_DUNGEON = "EVENT_STORY1_2";
    public const string KEY_STORY_1_END_CUTSCENE = "EVENT_STORY1_3";
    
    // Level 2
    public const string KEY_STORY_2_START_CUTSCENE = "EVENT_STORY2_1";
    public const string KEY_STORY_2_END_CUTSCENE = "EVENT_STORY2_2";
    
    // Level 3
    public const string KEY_STORY_3_START_CUTSCENE = "EVENT_STORY3_1";
    public const string KEY_STORY_3_END_CUTSCENE = "EVENT_STORY3_2";

    // Level 4
    public const string KEY_STORY_4_START_CUTSCENE = "EVENT_STORY4_1";

    // Ending
    public const string KEY_STORY_ENDING_CUTSCENE = "EVENT_STORY4_2";
    public const string KEY_STORY_ENDING_AFTER_CUTSCENE = "EVENT_STORY5";

    // Other cutscenes
    public const string KEY_CUTSCENE_DILUC = "EVENT_DILUC";
    public const string KEY_CUTSCENE_SHOP = "EVENT_SHOP";
    public const string KEY_CUTSCENE_SAVE = "EVENT_SAVE";
    public const string KEY_CUTSCENE_DEATH = "EVENT_DEATH";

    // Story data
    public static readonly Dictionary<String, String> CUTSCENES = new()
    {
        // Non story cutscene
        { KEY_CUTSCENE_DILUC, "Data/Cutscene/Test_Cutscene" },
        { KEY_CUTSCENE_DEATH, "Data/Cutscene/Death" },
        { KEY_CUTSCENE_SHOP, "Data/Cutscene/ShopGeneric" },
        { KEY_CUTSCENE_SAVE, "Data/Cutscene/SaveGeneric" },

        // Story cutscenes
        { KEY_STORY_1_START_CUTSCENE, "Data/Cutscene/Story1_1" },
        { KEY_STORY_1_END_CUTSCENE, "Data/Cutscene/Story1_2" },
        { KEY_STORY_2_START_CUTSCENE, "Data/Cutscene/Story2_1" },
        { KEY_STORY_2_END_CUTSCENE, "Data/Cutscene/Story2_2" },
        { KEY_STORY_3_START_CUTSCENE, "Data/Cutscene/Story3_1" },
        { KEY_STORY_3_END_CUTSCENE, "Data/Cutscene/Story3_2" },
        { KEY_STORY_4_START_CUTSCENE, "Data/Cutscene/Story4_1" },
        { KEY_STORY_ENDING_CUTSCENE, "Data/Cutscene/Story4_2" },
        { KEY_STORY_ENDING_AFTER_CUTSCENE, "Data/Cutscene/Story5" },
    };

    public static readonly List<String> EVENTS = new()
    {
        KEY_STORY_1_START_CUTSCENE,
        KEY_STORY_1_ENTER_DUNGEON,
        KEY_STORY_1_END_CUTSCENE,
        KEY_STORY_2_START_CUTSCENE,
        KEY_STORY_2_END_CUTSCENE,
        KEY_STORY_3_START_CUTSCENE,
        KEY_STORY_3_END_CUTSCENE,
        KEY_STORY_4_START_CUTSCENE,
        KEY_STORY_ENDING_CUTSCENE,
        KEY_STORY_ENDING_AFTER_CUTSCENE,

        KEY_CUTSCENE_SHOP,
        KEY_CUTSCENE_SAVE
    };
}