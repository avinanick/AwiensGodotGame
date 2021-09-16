using Godot;
using System;

public class VictoryInterface : PanelContainer
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Signal]
	public delegate void GameContinued();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void AddKillIcon() {
		
	}
	
	private void ClearKillIcons() {
		// I do not like these godot collections, the documentation is spotty
		GridContainer iconContainer = GetNode<GridContainer>("VBoxContainer/HBoxContainer/KillCountContainer/KillCountIconGrid");
		Godot.Collections.Array icons = iconContainer.GetChildren();
		for(int i = 0; i < icons.Count; i++) {
			if(icons[i] is Node nextIcon) {
				nextIcon.QueueFree();
			}
		}
	}

	public void ContinueGame() {
		Visible = false;
		ClearKillIcons();
		GetNode<CampaignTracker>("/root/CampaignTrackerAL").ConfirmLevelPoints();
		EmitSignal(nameof(GameContinued));
	}
	
	public void EnemyDefeated() {
		
	}

	public void PlayerDefeat() {
		// this should never actually be called, mostly just in case of error 
		// elsewhere
		Visible = false;
	}
	
	public void PlayerVictory() {
		UpdateScore();
		Input.SetMouseMode(Input.MouseMode.Visible);
		Visible = true;
	}
	
	public void QuitGame() {
		// I'm going to put a lot of this functionality into an autoload, since
		// several interfaces share this
	}
	
	public void SaveAndQuit() {
		// I'm going to put a lot of this functionality into an autoload, since
		// several interfaces share this
		GetNode<CampaignTracker>("/root/CampaignTrackerAL").ConfirmLevelPoints();
		GetNode<SaveGuardian>("/root/SaveGuardianAL").SaveGame();
	}
	
	private void UpdateScore() {
		
	}
}
