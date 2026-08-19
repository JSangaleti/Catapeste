using Godot;

namespace Catapeste;

public partial class LaunchMeter : Control
{
    private const float SweepSpeed = 0.72f;
    private float _direction = 1.0f;

    public float Power { get; private set; } = 0.08f;
    public bool IsRunning { get; private set; } = true;

    public override void _Process(double delta)
    {
        if (!IsRunning)
            return;

        Power += _direction * SweepSpeed * (float)delta;
        if (Power >= 1.0f || Power <= 0.0f)
        {
            Power = Mathf.Clamp(Power, 0.0f, 1.0f);
            _direction *= -1.0f;
        }

        QueueRedraw();
    }

    public void Restart()
    {
        Power = 0.08f;
        _direction = 1.0f;
        IsRunning = true;
        QueueRedraw();
    }

    public void Lock()
    {
        IsRunning = false;
        QueueRedraw();
    }

    public string GetRating()
    {
        if (Power < 0.35f)
            return "Fraco";
        if (Power < 0.65f)
            return "Médio";
        if (Power < 0.85f)
            return "Forte";
        return "Máximo";
    }

    public override void _Draw()
    {
        Vector2 centre = new(Size.X * 0.5f, Size.Y - 18.0f);
        float radius = Mathf.Min(Size.X * 0.38f, Size.Y - 42.0f);

        DrawArc(centre, radius, Mathf.Pi, Mathf.Pi * 1.35f, 28, new Color("#7da35a"), 27.0f, true);
        DrawArc(centre, radius, Mathf.Pi * 1.35f, Mathf.Pi * 1.65f, 24, new Color("#e2c44f"), 27.0f, true);
        DrawArc(centre, radius, Mathf.Pi * 1.65f, Mathf.Pi * 1.85f, 20, new Color("#db843d"), 27.0f, true);
        DrawArc(centre, radius, Mathf.Pi * 1.85f, Mathf.Tau, 18, new Color("#b83c35"), 27.0f, true);

        float needleAngle = Mathf.Pi + Power * Mathf.Pi;
        Vector2 needleTip = centre + Vector2.FromAngle(needleAngle) * (radius - 7.0f);
        DrawLine(centre, needleTip, new Color("#292725"), 6.0f, true);
        DrawCircle(centre, 11.0f, new Color("#292725"));
        DrawCircle(centre, 5.0f, new Color("#f1ddaa"));
    }
}
