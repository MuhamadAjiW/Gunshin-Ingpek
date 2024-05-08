using System;
using System.Collections.Generic;

// Configs should only contain constants and static readonly classes
public static class StoryConfig
{
    // Level 1
    public const string KEY_STORY_1_START_CUTSCENE = "EVENT_STORY1";
    public const string KEY_STORY_1_ENTER_DUNGEON = "EVENT_STORY1_1";
    public const string KEY_STORY_1_END_CUTSCENE = "EVENT_STORY1_2";
    
    // Level 2
    public const string KEY_STORY_2_END_CUTSCENE = "EVENT_STORY2";
    
    // Level 3
    public const string KEY_STORY_3_END_CUTSCENE = "EVENT_STORY3";

    // Ending
    public const string KEY_STORY_ENDING_CUTSCENE = "EVENT_STORY4";
    public const string KEY_STORY_ENDING_AFTER_CUTSCENE = "EVENT_STORY5";

    // Other cutscenes
    public const string KEY_CUTSCENE_DILUC = "EVENT_DILUC";
    public const string KEY_CUTSCENE_DEATH = "EVENT_DEATH";

    // Story data
    public static readonly Dictionary<String, String> CUTSCENES = new()
    {
        // Non story cutscene
        { KEY_CUTSCENE_DILUC, "Data/Cutscene/Test_Cutscene" },
        { KEY_CUTSCENE_DEATH, "Data/Cutscene/Death" },

        // Story cutscenes
        { KEY_STORY_1_START_CUTSCENE, "Data/Cutscene/Story1" },
        { KEY_STORY_1_END_CUTSCENE, "Data/Cutscene/Story2" },
        { KEY_STORY_2_END_CUTSCENE, "Data/Cutscene/Story3" },
        { KEY_STORY_3_END_CUTSCENE, "Data/Cutscene/Story4" },
        { KEY_STORY_ENDING_CUTSCENE, "Data/Cutscene/Story5" },
        { KEY_STORY_ENDING_AFTER_CUTSCENE, "Data/Cutscene/Story6" },
    };

    public static readonly List<String> STORY_EVENTS = new()
    {
        KEY_STORY_1_START_CUTSCENE,
        KEY_STORY_1_ENTER_DUNGEON,
        KEY_STORY_1_END_CUTSCENE,
        KEY_STORY_2_END_CUTSCENE,
        KEY_STORY_3_END_CUTSCENE,
        KEY_STORY_ENDING_CUTSCENE,
        KEY_STORY_ENDING_AFTER_CUTSCENE,
    };
}