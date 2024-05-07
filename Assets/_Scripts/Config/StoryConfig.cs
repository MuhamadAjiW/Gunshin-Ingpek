using System;
using System.Collections.Generic;

// Configs should only contain constants and static readonly classes
public static class StoryConfig
{
    public const string KEY_TEST_EVENT = "TEST_EVENT";

    // Story data
    public static readonly Dictionary<String, String> STORY_EVENTS = new()
    {
        { "TEST_EVENT", "Data/Cutscene/Test_Cutscene" }
    };

    public static List<string> GetStoryEvents()
    {
        // Return the keys of the STORY_EVENTS dictionary
        return new List<string>(STORY_EVENTS.Keys);
    }
}