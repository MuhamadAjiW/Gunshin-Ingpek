using System.Collections.Generic;

public static class GameConfig{
    // Default damaged state delay value
    public static float DAMAGED_DELAY_DURATION = 1.0f;

    // Movement lerp constants
    public static float MOVEMENT_SMOOTHING = 0.14f;
    public static float MOVEMENT_SMOOTHING_JUMP_SNAPSHOT = 0.14f;
    public static float ROTATION_SMOOTHING = 720;

    // Difficulty multipliers
    private static readonly DifficultyData EasyData = new(){
        EnemyDamageMultiplier = 0.5f,
        EnemyHealthMultiplier = 0.5f,
        PlayerDamageMultiplier = 2f,
        PlayerHealthMultiplier = 2f,
    };
    private static readonly DifficultyData MediumData = new(){
        EnemyDamageMultiplier = 0.5f,
        EnemyHealthMultiplier = 0.5f,
        PlayerDamageMultiplier = 2f,
        PlayerHealthMultiplier = 2f,
    };
    private static readonly DifficultyData HardData = new(){
        EnemyDamageMultiplier = 2f,
        EnemyHealthMultiplier = 2f,
        PlayerDamageMultiplier = 0.5f,
        PlayerHealthMultiplier = 0.5f,
    };

    public static Dictionary<DifficultyType, DifficultyData> DIFFICULTY_MODIFIERS = new(){
        { DifficultyType.EASY, EasyData},
        { DifficultyType.NORMAL, MediumData},
        { DifficultyType.HARD, HardData}
    };
}