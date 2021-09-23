using Godot;
using System;

public class ThreatsPreviewInterface : MarginContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private PackedScene IconPackedScene = GD.Load<PackedScene>("res://GameObjects/Interfaces/ThreatIcon.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void AddThreat(string threatName, bool isAlien) {
        // This should get the icon for the threat and add it to the appropriate list
    }

    public void AttachAtLocation(Vector2 location) {

    }

    public void ClearThreats() {
        // Remove all threats, used when switching to another city
    }

    public void SetCityName(string cityName) {

    }
}
