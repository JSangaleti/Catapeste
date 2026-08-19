using Godot;

namespace Catapeste;

public partial class Plebeian : CharacterBody2D
{
    [Signal]
    public delegate void LandedEventHandler();

    private const float Gravity = 900.0f;
    private const float FlightControlAcceleration = 1180.0f;
    private const float MaximumRiseSpeed = -500.0f;
    private const float MaximumFallSpeed = 680.0f;

    private bool _isFlying;
    private float _launchOriginX;

    public float DistanceTravelled => Mathf.Max(0.0f, GlobalPosition.X - _launchOriginX);
    public bool IsFlying => _isFlying;

    public override void _PhysicsProcess(double delta)
    {
        if (!_isFlying)
            return;

        float step = (float)delta;
        float verticalInput = Input.GetAxis("move_up", "move_down");
        float verticalSpeed = Velocity.Y + (Gravity + verticalInput * FlightControlAcceleration) * step;

        Velocity = new Vector2(
            Mathf.Max(250.0f, Velocity.X - 16.0f * step),
            Mathf.Clamp(verticalSpeed, MaximumRiseSpeed, MaximumFallSpeed)
        );

        MoveAndSlide();
        Rotation = Mathf.LerpAngle(Rotation, Velocity.Angle() * 0.18f, 5.0f * step);

        if (GlobalPosition.Y < 82.0f)
            GlobalPosition = new Vector2(GlobalPosition.X, 82.0f);

        if (IsOnFloor() && Velocity.Y >= 0.0f)
        {
            _isFlying = false;
            EmitSignal(SignalName.Landed);
        }
    }

    public void ResetForAttempt(Vector2 launchPosition)
    {
        _isFlying = false;
        GlobalPosition = launchPosition;
        Velocity = Vector2.Zero;
        Rotation = 0.0f;
        _launchOriginX = launchPosition.X;
    }

    public void Launch(float power, float launchMultiplier)
    {
        float horizontalSpeed = Mathf.Lerp(430.0f, 780.0f, power) * launchMultiplier;
        float verticalSpeed = Mathf.Lerp(-430.0f, -610.0f, power);
        Velocity = new Vector2(horizontalSpeed, verticalSpeed);
        _launchOriginX = GlobalPosition.X;
        _isFlying = true;
    }

    public void Stop()
    {
        _isFlying = false;
        Velocity = Vector2.Zero;
    }

    public override void _Draw()
    {
        DrawCircle(new Vector2(13.0f, -18.0f), 11.0f, new Color("#e7b978"));
        DrawCircle(new Vector2(17.0f, -21.0f), 2.0f, new Color("#2f2924"));
        DrawPolygon(
            new[]
            {
                new Vector2(-17.0f, -13.0f),
                new Vector2(11.0f, -13.0f),
                new Vector2(17.0f, 14.0f),
                new Vector2(-14.0f, 17.0f),
            },
            new[] { new Color("#7f9b4f") }
        );
        DrawLine(new Vector2(-8.0f, 14.0f), new Vector2(-22.0f, 29.0f), new Color("#4c3c30"), 6.0f, true);
        DrawLine(new Vector2(9.0f, 14.0f), new Vector2(24.0f, 26.0f), new Color("#4c3c30"), 6.0f, true);
        DrawLine(new Vector2(-13.0f, -5.0f), new Vector2(-28.0f, 4.0f), new Color("#e7b978"), 5.0f, true);
        DrawArc(new Vector2(2.0f, -5.0f), 25.0f, 0.25f, 2.7f, 18, new Color("#d7e6c0"), 2.0f, true);
    }
}
