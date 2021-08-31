using Godot;
using System;

public class DefeatInterface : PanelContainer
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		Visible = false;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void PlayerDefeated() {
		Visible = true;
	}
	
	public void QuitGame() {
		// This should delete the current save and load the main menu, should I
		// impliment this in the autoload since the pause menu has an identical
		// need?
	}
	
	public void RetryGame() {
		// This should delete the current save and reload to level one for a new
		// game
	}
	
	public void UpdateScore(int pointsEarned) {
		
	}
	
	public void UpdateTimeRemaining(int secondsLeft) {
		
	}
}
