using _Scripts.Core.Game.Data;

public class Diluc_NPC : NPCController
{
    // Constructor
    protected void Start()
    {
<<<<<<< HEAD
        if(GameSaveData.Instance.storyData.IsEventComplete(StoryConfig.KEY_STORY_1_ENTER_DUNGEON))
=======
        if (GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_1_END_CUTSCENE))
        {
            controlledNPC.transform.SetPositionAndRotation(positions[2].position, positions[2].rotation);
        }
        else if (GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_1_ENTER_DUNGEON))
>>>>>>> ffe29481 (feat: base interactables for shop and save)
        {
            controlledNPC.transform.SetPositionAndRotation(positions[1].position, positions[1].rotation);
        }
        else
        {
            controlledNPC.transform.SetPositionAndRotation(positions[0].position, positions[0].rotation);
            GameController.Instance.OnGameEvent += KeepTrack;
        }
    }

    protected void KeepTrack(string eventId, System.Object info)
    {
        if(eventId == StoryConfig.KEY_STORY_1_ENTER_DUNGEON)
        {
            controlledNPC.transform.SetPositionAndRotation(positions[1].position, positions[1].rotation);
        }
        else if (eventId == StoryConfig.KEY_STORY_1_END_CUTSCENE)
        {
            controlledNPC.transform.SetPositionAndRotation(positions[2].position, positions[2].rotation);
        }
    }
}