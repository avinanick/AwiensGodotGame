using Godot;
using System;

public class LevelCountdown : MarginContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        CallDeferred(nameof(StartCountdown));
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void EndCountdown() {
        GetTree().Paused = false;
    }

    private void StartCountdown() {
        GetTree().Paused = true;
        GetNode<AnimationPlayer>("AnimationPlayer").Play("Countdown");
    }
}
