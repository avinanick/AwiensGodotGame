using Godot;
using System;

public class AutoTurret : Turret
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Ship CurrentTarget = null;

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

    protected void AttemptAttack() {

    }

    protected void FindTarget() {

    }

    public void OnTargetEnterRange(Node body) {
        if(body.IsInGroup("Aliens")) {
            CurrentTarget = (Ship)body;
        }
    }

    protected void TrackTarget() {

    }
}
