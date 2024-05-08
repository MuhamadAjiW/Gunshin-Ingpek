using _Scripts.Core.Game.Data;

public class Diluc_LV1 : NPCController
{
    // Constructor
    protected void Start()
    {
        if(GameSaveData.Instance.storyData.IsEventComplete(StoryConfig.KEY_STORY_1_ENTER_DUNGEON))
        {
            controlledNPC.transform.SetPositionAndRotation(positions[1].position, positions[1].rotation);
        }
        else
        {
            controlledNPC.transform.SetPositionAndRotation(positions[0].position, positions[0].rotation);
            GameController.Instance.OnGameEvent += OnDungeonEnter;
        }
    }

    protected void OnDungeonEnter(string eventId)
    {
        if(eventId == StoryConfig.KEY_STORY_1_ENTER_DUNGEON)
        {
            GameController.Instance.OnGameEvent -= OnDungeonEnter;
            controlledNPC.transform.SetPositionAndRotation(positions[1].position, positions[1].rotation);
        }
    }
}