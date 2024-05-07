using System;
using System.Collections.Generic;

// Configs should only contain constants and static readonly classes
public static class StoryConfig
{
    public const string KEY_TEST_EVENT = "EVENT_TEST";
    public const string KEY_STORY1 = "EVENT_STORY1";
    public const string KEY_STORY2 = "EVENT_STORY2";
    public const string KEY_STORY3 = "EVENT_STORY3";
    public const string KEY_STORY4 = "EVENT_STORY4";
    public const string KEY_STORY5 = "EVENT_STORY5";
    public const string KEY_STORY6 = "EVENT_STORY6";
    public const string KEY_DEATH = "EVENT_DEATH";

    // Story data
    public static readonly Dictionary<String, String> STORY_EVENTS = new()
    {
        { KEY_TEST_EVENT, "Data/Cutscene/Test_Cutscene" },
        { KEY_STORY1, "Data/Cutscene/Story1" },
        { KEY_STORY2, "Data/Cutscene/Story2" },
        { KEY_STORY3, "Data/Cutscene/Story3" },
        { KEY_STORY4, "Data/Cutscene/Story4" },
        { KEY_STORY5, "Data/Cutscene/Story5" },
        { KEY_STORY6, "Data/Cutscene/Story6" },
        { KEY_DEATH, "Data/Cutscene/Death" },
    };

    public static List<string> GetStoryEvents()
    {
        // Return the keys of the STORY_EVENTS dictionary
        return new List<string>(STORY_EVENTS.Keys);
    }
}