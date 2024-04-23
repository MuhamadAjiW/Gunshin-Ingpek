using System.Collections.Generic;

// Configs should only contain constants and static readonly classes
public static class GameConfig
{
    // Default damaged state delay value
    public const float DAMAGED_DELAY_DURATION = 1.0f;

    // Movement lerp constants
    public const float MOVEMENT_SMOOTHING = 0.14f;
    public const float ROTATION_SMOOTHING = 720;
    public const float CAMERA_MOUSE_VERTICAL_MAX = 60;

    // Difficulty multipliers
    private static readonly DifficultyData EasyData = new()
    {
        EnemyDamageMultiplier = 0.5f,
        EnemyHealthMultiplier = 0.5f,
        PlayerDamageMultiplier = 2f,
        PlayerHealthMultiplier = 2f,
    };
    private static readonly DifficultyData MediumData = new()
    {
        EnemyDamageMultiplier = 0.5f,
        EnemyHealthMultiplier = 0.5f,
        PlayerDamageMultiplier = 2f,
        PlayerHealthMultiplier = 2f,
    };
    private static readonly DifficultyData HardData = new()
    {
        EnemyDamageMultiplier = 2f,
        EnemyHealthMultiplier = 2f,
        PlayerDamageMultiplier = 0.5f,
        PlayerHealthMultiplier = 0.5f,
    };

    public static readonly Dictionary<DifficultyType, DifficultyData> DIFFICULTY_MODIFIERS = new()
    {
        { DifficultyType.EASY, EasyData},
        { DifficultyType.NORMAL, MediumData},
        { DifficultyType.HARD, HardData}
    };
}