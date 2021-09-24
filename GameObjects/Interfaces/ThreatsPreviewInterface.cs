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
        Visible = false;
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
        Node aliensContainer = GetNode<Node>("PanelContainer/VBoxContainer/HBoxContainer");
        Node modifiersContainer = GetNode<Node>("PanelContainer/VBoxContainer/HBoxContainer2");
        Godot.Collections.Array<Node> alienIcons = new Godot.Collections.Array<Node>(aliensContainer.GetChildren());
        Godot.Collections.Array<Node> modifierIcons = new Godot.Collections.Array<Node>(modifiersContainer.GetChildren());
        for(int i = 0; i < alienIcons.Count; i++) {
            alienIcons[i].QueueFree();
        }
        for(int i = 0; i < modifierIcons.Count; i++) {
            modifierIcons[i].QueueFree();
        }
    }

    public void SetCityName(string cityName) {

    }
}
