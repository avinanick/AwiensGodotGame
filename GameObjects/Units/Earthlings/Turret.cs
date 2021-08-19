using Godot;
using System;

public class Turret : Destructible
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private AttackerComponent Blah;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AttackerComponent attacker = GetNode<AttackerComponent>("Attacker");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
