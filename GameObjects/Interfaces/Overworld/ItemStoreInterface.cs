using Godot;
using System;

public class ItemStoreInterface : MarginContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        PopulateInventory();
        RandomizeAvailable();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void MakePurchase(string item) {

    }

    public void MakeSale(string item) {

    }

    private void PopulateInventory() {

    }

    private void RandomizeAvailable() {

    }
}
