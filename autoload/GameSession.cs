using Godot;

namespace Catapeste;

public partial class GameSession : Node
{
    private const int MaximumCatapultLevel = 5;

    public int Money { get; private set; }
    public int CatapultLevel { get; private set; }
    public float LaunchMultiplier => 1.0f + CatapultLevel * 0.08f;
    public int UpgradeCost => CatapultLevel >= MaximumCatapultLevel
        ? 0
        : 20 + CatapultLevel * 25;
    public bool IsCatapultMaxed => CatapultLevel >= MaximumCatapultLevel;

    public int CompleteAttempt(float distanceMetres, bool victory)
    {
        int reward = Mathf.Max(1, Mathf.FloorToInt(distanceMetres / 8.0f));
        if (victory)
            reward += 100;

        Money += reward;
        return reward;
    }

    public bool TryUpgradeCatapult()
    {
        if (IsCatapultMaxed || Money < UpgradeCost)
            return false;

        Money -= UpgradeCost;
        CatapultLevel++;
        return true;
    }
}
