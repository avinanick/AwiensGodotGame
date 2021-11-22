using Godot;
using System;

public class HolographicDecoyItem : Item
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private PackedScene HologramScene = GD.Load<PackedScene>("res://GameObjects/Units/Earthlings/Hologram.tscn");

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
        // Need to figure out a way to set a position for this
        Godot.Collections.Array<Spatial> spawnPoints = new Godot.Collections.Array<Spatial>(GetTree().GetNodesInGroup("HoloPoint"));
        spawnPoints.Shuffle();
        for(int i = 0; i < spawnPoints.Count; i++) {
            if(spawnPoints[i].GetChildCount() < 1) {
                // Spawn the hologram here and return
                Destructible newHologram = HologramScene.Instance<Destructible>();
                spawnPoints[i].AddChild(newHologram);
                newHologram.GlobalTransform = spawnPoints[i].GlobalTransform;
                return;
            }
        }
        // If we get here, there are no remaining spawn points
        GD.Print("No spawn points available");
        // I should likely re-add the item that would have been spawned to the inventory here
        GetNode<UserInventory>("/root/UserInventoryAL").ReturnItem("Holographic Decoy");
    }
}
