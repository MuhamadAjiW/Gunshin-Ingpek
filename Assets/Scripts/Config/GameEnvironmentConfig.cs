// Configs should only contain constants and static readonly classes
public static class GameEnvironmentConfig{
    // Tags
    public const string TAG_UNTAGGED = "Untagged";
    public const string TAG_PLAYER = "Player";
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_COLLECTIBLES = "Collectibles";
    public const string TAG_GROUND = "Ground";

    // Layers
    public const string LAYER_PLAYER = "Player";
    public const string LAYER_ENEMY = "Enemy";
    public const string LAYER_PLAYER_ATTACK = "PlayerAttack";
    public const string LAYER_ENEMY_ATTACK = "EnemyAttack";
    public const string LAYER_ENVIRONMENT_ATTACK = "EnvironmentAttack";
    public const string LAYER_COLLECTIBLE = "Collectible";
}