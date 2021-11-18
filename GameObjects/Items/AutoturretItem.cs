using Godot;
using System;

public class AutoturretItem : Item
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private PackedScene turretScene = GD.Load<PackedScene>("res://GameObjects/Units/Earthlings/AutoTurret.tscn");

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

    public override void Deploy() {
        Godot.Collections.Array<Avatar> playerAvatar = new Godot.Collections.Array<Avatar>(GetTree().GetNodesInGroup("Player"));
        if(playerAvatar.Count > 0) {
            playerAvatar[0].DeployWeaponAddon(turretScene.Instance<Turret>());
        }
        else {
            GD.Print("Error: No player avatar found");
        }
        QueueFree();
    }
}
