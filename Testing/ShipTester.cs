using Godot;
using System;

public class ShipTester : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    private PackedScene ShipScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        GetNode<Timer>("Timer").Start();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void OnShipTimerTimeout() {
        CallDeferred(nameof(SpawnShip));
    }

    public void SpawnShip() {
        CombatShip newAlien = ShipScene.Instance<CombatShip>();
        GetTree().CurrentScene.AddChild(newAlien);
        newAlien.GlobalTransform = GlobalTransform;
        newAlien.SpawnShip();
    }
}
