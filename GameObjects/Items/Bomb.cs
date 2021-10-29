using Godot;
using System;

public class Bomb : Item
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

    protected override void Activate() {
        // This should get all destructibles in the alien group and deal heavy damage
        // to them all
        // Should I deal heavy damage or just call destroy self on each?
        base.Activate();
        Godot.Collections.Array<Destructible> alienTargets = new Godot.Collections.Array<Destructible>(GetTree().GetNodesInGroup("Aliens"));
        for(int i = 0; i < alienTargets.Count; i++) {
            // I may go with dealing damage instead, but for now lets go with instant kills.
            alienTargets[i].DestroySelf();
        }
        QueueFree();
    }
}
