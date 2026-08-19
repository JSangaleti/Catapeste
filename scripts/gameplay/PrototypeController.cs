using Godot;

namespace Catapeste;

public partial class PrototypeController : Node2D
{
    private enum AttemptPhase
    {
        Aiming,
        Flying,
        Results,
    }

    private static readonly Vector2 LaunchPosition = new(220.0f, 535.0f);

    private Plebeian _plebeian = null!;
    private PrototypeCourse _course = null!;
    private LaunchMeter _launchMeter = null!;
    private GameSession _session = null!;
    private Control _launchPanel = null!;
    private Control _resultsPanel = null!;
    private Label _distanceLabel = null!;
    private Label _moneyLabel = null!;
    private Label _levelLabel = null!;
    private Label _statusLabel = null!;
    private Label _resultTitle = null!;
    private Label _resultSummary = null!;
    private Button _upgradeButton = null!;
    private Button _newLaunchButton = null!;
    private AttemptPhase _phase;
    private float _distanceMetres;

    public override void _Ready()
    {
        _plebeian = GetNode<Plebeian>("Plebeian");
        _course = GetNode<PrototypeCourse>("Course");
        _launchMeter = GetNode<LaunchMeter>("HUD/LaunchPanel/LaunchMeter");
        _session = GetNode<GameSession>("/root/GameSession");
        _launchPanel = GetNode<Control>("HUD/LaunchPanel");
        _resultsPanel = GetNode<Control>("HUD/ResultsPanel");
        _distanceLabel = GetNode<Label>("HUD/TopBar/Margin/Stats/Distance");
        _moneyLabel = GetNode<Label>("HUD/TopBar/Margin/Stats/Money");
        _levelLabel = GetNode<Label>("HUD/TopBar/Margin/Stats/Level");
        _statusLabel = GetNode<Label>("HUD/Status");
        _resultTitle = GetNode<Label>("HUD/ResultsPanel/Content/Title");
        _resultSummary = GetNode<Label>("HUD/ResultsPanel/Content/Summary");
        _upgradeButton = GetNode<Button>("HUD/ResultsPanel/Content/Actions/Upgrade");
        _newLaunchButton = GetNode<Button>("HUD/ResultsPanel/Content/Actions/NewLaunch");

        _plebeian.Landed += OnPlebeianLanded;
        _upgradeButton.Pressed += OnUpgradePressed;
        _newLaunchButton.Pressed += PrepareAttempt;

        foreach (Node node in GetTree().GetNodesInGroup("hazard"))
        {
            if (node is Area2D hazard)
                hazard.BodyEntered += OnHazardBodyEntered;
        }

        PrepareAttempt();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (_phase == AttemptPhase.Aiming && @event.IsActionPressed("launch"))
        {
            StartFlight();
            GetViewport().SetInputAsHandled();
        }
        else if (_phase == AttemptPhase.Results && @event.IsActionPressed("restart_attempt"))
        {
            PrepareAttempt();
            GetViewport().SetInputAsHandled();
        }
    }

    public override void _Process(double delta)
    {
        if (_phase != AttemptPhase.Flying)
            return;

        _distanceMetres = _plebeian.DistanceTravelled / 10.0f;
        _distanceLabel.Text = $"DISTÂNCIA  {_distanceMetres:0} m";

        if (_plebeian.GlobalPosition.X >= _course.FinishX)
            FinishAttempt(true);
    }

    private void PrepareAttempt()
    {
        _phase = AttemptPhase.Aiming;
        _distanceMetres = 0.0f;
        _plebeian.ResetForAttempt(LaunchPosition);
        _launchMeter.Restart();
        _launchPanel.Visible = true;
        _resultsPanel.Visible = false;
        _statusLabel.Text = "ESPAÇO para travar a força";
        RefreshHud();
    }

    private void StartFlight()
    {
        _launchMeter.Lock();
        string rating = _launchMeter.GetRating();
        _launchPanel.Visible = false;
        _statusLabel.Text = $"{rating.ToUpperInvariant()}  •  W / S para controlar a altura";
        _phase = AttemptPhase.Flying;
        _plebeian.Launch(_launchMeter.Power, _session.LaunchMultiplier);
    }

    private void OnPlebeianLanded()
    {
        FinishAttempt(false);
    }

    private void OnHazardBodyEntered(Node2D body)
    {
        if (_phase == AttemptPhase.Flying && body == _plebeian)
            FinishAttempt(false);
    }

    private void FinishAttempt(bool victory)
    {
        if (_phase != AttemptPhase.Flying)
            return;

        _phase = AttemptPhase.Results;
        _plebeian.Stop();
        int reward = _session.CompleteAttempt(_distanceMetres, victory);

        _resultTitle.Text = victory ? "A FORTALEZA FOI ALCANÇADA" : "FIM DA TENTATIVA";
        _resultSummary.Text = $"Distância: {_distanceMetres:0} m\nRecompensa: {reward} moedas";
        _statusLabel.Text = victory ? "Objetivo do protótipo concluído" : "R para tentar novamente";
        _resultsPanel.Visible = true;
        RefreshHud();
    }

    private void OnUpgradePressed()
    {
        if (!_session.TryUpgradeCatapult())
            return;

        RefreshHud();
    }

    private void RefreshHud()
    {
        _distanceLabel.Text = $"DISTÂNCIA  {_distanceMetres:0} m";
        _moneyLabel.Text = $"MOEDAS  {_session.Money}";
        _levelLabel.Text = $"CATAPULTA  NV. {_session.CatapultLevel + 1}";

        if (_session.IsCatapultMaxed)
        {
            _upgradeButton.Text = "CATAPULTA NO MÁXIMO";
            _upgradeButton.Disabled = true;
        }
        else
        {
            _upgradeButton.Text = $"MELHORAR  •  {_session.UpgradeCost} moedas";
            _upgradeButton.Disabled = _session.Money < _session.UpgradeCost;
        }
    }
}
