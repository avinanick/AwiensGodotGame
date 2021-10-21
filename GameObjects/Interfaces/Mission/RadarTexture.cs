using Godot;
using System;

public class RadarTexture : TextureRect
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	private PackedScene IndicatorScene = (PackedScene)GD.Load("res://GameObjects/Interfaces/Mission/RadarIndicator.tscn");

	private Light2D PlayerView;
	private Avatar PlayerAvatar;
	private bool Jammed = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		PlayerView = GetNode<Light2D>("PlayerView");
		CheckEnhancements();
		CallDeferred("PopulateIndicators");
		CallDeferred("AssignPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		base._Process(delta);
		UpdateAvatarIndicator();
	}

	public void AssignPlayer() {
		PlayerAvatar = new Godot.Collections.Array<Avatar>(GetTree().GetNodesInGroup("Player"))[0];
	}

	protected void CheckEnhancements() {
        CampaignTracker tracker = GetNode<CampaignTracker>("/root/CampaignTrackerAL");
        int jammingCount = tracker.CheckForEnhancement("Jamming");
        if(jammingCount > 0) {
			// Disable the radar
			Jammed = true;
		}
    }

	public void DestructibleSpawned(Destructible newUnit) {
		RadarIndicator newIndicator = (RadarIndicator)IndicatorScene.Instance();
		AddChild(newIndicator);
		newIndicator.AssignUnit(newUnit);
	}

	private void PopulateIndicators() {
		Godot.Collections.Array<Destructible> units = new Godot.Collections.Array<Destructible>(GetTree().GetNodesInGroup("Destructible"));
		for(int i = 0; i < units.Count; i++) {
			DestructibleSpawned(units[i]);
		}
	}

	private void UpdateAvatarIndicator() {
		if(PlayerAvatar != null) {
            Vector3 unitPosition = PlayerAvatar.GlobalTransform.origin;
            Vector2 convertedPosition = new Vector2();
            convertedPosition.x = (-1) * unitPosition.x * 1 + 50;
            convertedPosition.y = (-1) * unitPosition.z * 1 + 50;
            PlayerView.Position = convertedPosition;
			PlayerView.RotationDegrees = (-1) * PlayerAvatar.RotationDegrees.y;
        }
	}
}
