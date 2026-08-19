using Godot;

namespace Catapeste;

public partial class PrototypeCourse : Node2D
{
    [Export]
    public float FinishX { get; set; } = 5900.0f;

    public override void _Draw()
    {
        DrawRect(new Rect2(-500.0f, -500.0f, 7100.0f, 1100.0f), new Color("#84c5d3"));
        DrawCircle(new Vector2(800.0f, 130.0f), 58.0f, new Color("#f7d67a"));

        for (int x = -200; x < 6500; x += 720)
        {
            DrawCircle(new Vector2(x + 100.0f, 170.0f), 42.0f, new Color("#e7f2e8"));
            DrawCircle(new Vector2(x + 145.0f, 154.0f), 55.0f, new Color("#e7f2e8"));
            DrawCircle(new Vector2(x + 198.0f, 174.0f), 38.0f, new Color("#e7f2e8"));
        }

        for (int x = -400; x < 6500; x += 430)
        {
            Vector2[] hill =
            {
                new Vector2(x, 600.0f),
                new Vector2(x + 210.0f, 380.0f),
                new Vector2(x + 430.0f, 600.0f),
            };
            DrawColoredPolygon(hill, new Color("#79956a"));
        }

        DrawRect(new Rect2(-500.0f, 600.0f, 7100.0f, 400.0f), new Color("#6d8a49"));
        DrawRect(new Rect2(-500.0f, 600.0f, 7100.0f, 13.0f), new Color("#a8bd62"));

        DrawCatapult(new Vector2(115.0f, 555.0f));
        DrawTree(new Vector2(1600.0f, 600.0f));
        DrawBird(new Vector2(2450.0f, 280.0f));
        DrawWatchtower(new Vector2(3380.0f, 600.0f));
        DrawArrow(new Vector2(4400.0f, 360.0f));
        DrawCannonBall(new Vector2(5100.0f, 470.0f));
        DrawCastle(new Vector2(5700.0f, 600.0f));
    }

    private void DrawCatapult(Vector2 basePosition)
    {
        Color wood = new("#755039");
        DrawCircle(basePosition + new Vector2(-45.0f, 15.0f), 24.0f, new Color("#40362d"));
        DrawCircle(basePosition + new Vector2(45.0f, 15.0f), 24.0f, new Color("#40362d"));
        DrawLine(basePosition + new Vector2(-55.0f, 5.0f), basePosition + new Vector2(55.0f, 5.0f), wood, 15.0f, true);
        DrawLine(basePosition, basePosition + new Vector2(82.0f, -116.0f), wood, 13.0f, true);
        DrawArc(basePosition + new Vector2(88.0f, -120.0f), 25.0f, -0.2f, 2.4f, 20, new Color("#4f453a"), 8.0f, true);
    }

    private void DrawTree(Vector2 basePosition)
    {
        DrawRect(new Rect2(basePosition + new Vector2(-17.0f, -155.0f), new Vector2(34.0f, 155.0f)), new Color("#65452f"));
        DrawCircle(basePosition + new Vector2(0.0f, -185.0f), 74.0f, new Color("#3f713f"));
        DrawCircle(basePosition + new Vector2(-42.0f, -148.0f), 48.0f, new Color("#4c8248"));
        DrawCircle(basePosition + new Vector2(43.0f, -145.0f), 52.0f, new Color("#4c8248"));
    }

    private void DrawBird(Vector2 position)
    {
        DrawArc(position + new Vector2(-18.0f, 0.0f), 22.0f, 3.55f, 5.95f, 18, new Color("#403a37"), 6.0f, true);
        DrawArc(position + new Vector2(18.0f, 0.0f), 22.0f, 3.48f, 5.85f, 18, new Color("#403a37"), 6.0f, true);
        DrawCircle(position, 8.0f, new Color("#d8cbb0"));
    }

    private void DrawWatchtower(Vector2 basePosition)
    {
        DrawRect(new Rect2(basePosition + new Vector2(-48.0f, -272.0f), new Vector2(96.0f, 272.0f)), new Color("#8c8273"));
        DrawRect(new Rect2(basePosition + new Vector2(-66.0f, -306.0f), new Vector2(132.0f, 45.0f)), new Color("#665f56"));
        for (int x = -60; x <= 40; x += 34)
            DrawRect(new Rect2(basePosition + new Vector2(x, -330.0f), new Vector2(21.0f, 30.0f)), new Color("#665f56"));
    }

    private void DrawArrow(Vector2 position)
    {
        DrawLine(position + new Vector2(-52.0f, 0.0f), position + new Vector2(43.0f, 0.0f), new Color("#5f4131"), 5.0f, true);
        DrawColoredPolygon(
            new[] { position + new Vector2(43.0f, -9.0f), position + new Vector2(62.0f, 0.0f), position + new Vector2(43.0f, 9.0f) },
            new Color("#44484a")
        );
    }

    private void DrawCannonBall(Vector2 position)
    {
        DrawCircle(position, 30.0f, new Color("#34383a"));
        DrawCircle(position + new Vector2(-9.0f, -10.0f), 7.0f, new Color("#62696b"));
    }

    private void DrawCastle(Vector2 basePosition)
    {
        Color stone = new("#756d65");
        DrawRect(new Rect2(basePosition + new Vector2(0.0f, -330.0f), new Vector2(520.0f, 330.0f)), stone);
        DrawRect(new Rect2(basePosition + new Vector2(-65.0f, -450.0f), new Vector2(145.0f, 450.0f)), new Color("#665f59"));
        DrawRect(new Rect2(basePosition + new Vector2(440.0f, -450.0f), new Vector2(145.0f, 450.0f)), new Color("#665f59"));
        DrawRect(new Rect2(basePosition + new Vector2(180.0f, -205.0f), new Vector2(155.0f, 205.0f)), new Color("#292725"));
        for (int x = -60; x < 570; x += 55)
            DrawRect(new Rect2(basePosition + new Vector2(x, -475.0f), new Vector2(34.0f, 34.0f)), new Color("#665f59"));
        DrawString(ThemeDB.FallbackFont, basePosition + new Vector2(135.0f, -360.0f), "FORTALEZA", HorizontalAlignment.Left, -1.0f, 28, new Color("#eee2c3"));
    }
}
