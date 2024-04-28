using System.Collections.Generic;

// Configs should only contain constants and static readonly classes
public static class GameConfig
{
    // Default damaged state delay value
    public const float DAMAGED_DELAY_DURATION = 1.0f;

    // Movement lerp constants
    public const float MOVEMENT_SMOOTHING = 0.14f;
    public const float MOVEMENT_FALL_SMOOTHING = 1.2f;
    public const float ROTATION_SMOOTHING = 720;
    public const float CAMERA_MOUSE_VERTICAL_MAX = 60;

    // Difficulty multipliers
    private static readonly DifficultyData EasyData = new()
    {
        enemyDamageMultiplier = 0.5f,
        enemyHealthMultiplier = 0.5f,
        playerDamageMultiplier = 2f,
        playerHealthMultiplier = 2f,
    };
    private static readonly DifficultyData MediumData = new()
    {
        enemyDamageMultiplier = 1f,
        enemyHealthMultiplier = 1f,
        playerDamageMultiplier = 1f,
        playerHealthMultiplier = 1f,
    };
    private static readonly DifficultyData HardData = new()
    {
        enemyDamageMultiplier = 2f,
        enemyHealthMultiplier = 2f,
        playerDamageMultiplier = 0.5f,
        playerHealthMultiplier = 0.5f,
    };

    public static readonly Dictionary<DifficultyType, DifficultyData> DIFFICULTY_MODIFIERS = new()
    {
        { DifficultyType.EASY, EasyData},
        { DifficultyType.NORMAL, MediumData},
        { DifficultyType.HARD, HardData}
    };
}