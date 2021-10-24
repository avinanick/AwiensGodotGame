using Godot;
using System;

public class Smokescreen : Particles
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

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

    public void Blast() {
        GetNode<Timer>("Timer").Start();
        Emitting = true;
    }

    public void Finish() {
        QueueFree();
    }
}
