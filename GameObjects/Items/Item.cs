using Godot;
using System;

public class Item : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    private float DeployTime = 1.5f;

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

    protected virtual void Activate() {

    }

    public virtual void Deploy() {
        // Items should be spawned in the center of the city and launched straight up
        // then activated
        Translation = new Vector3(0,0,0);
    }
}
