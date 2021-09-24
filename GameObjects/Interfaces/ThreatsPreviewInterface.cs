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
        ThreatIcon newIcon = (ThreatIcon)IconPackedScene.Instance();
        AlienData dataAL = GetNode<AlienData>("/root/AlienDataAL");
        Texture iconTexture = GD.Load<Texture>(dataAL.GetAlienIconPath(threatName));
        newIcon.SetIcon(iconTexture);
        if(isAlien) {
            GetNode("PanelContainer/VBoxContainer/HBoxContainer").AddChild(newIcon);
        }
        else {
            GetNode("PanelContainer/VBoxContainer/HBoxContainer2").AddChild(newIcon);
        }
    }

    public void AttachAtLocation(Vector2 location) {
        // I need to figure out the exact location I want this, until I do this will
        // just be a straightforward set
        RectPosition = location;
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
        GetNode<Label>("PanelContainer/VBoxContainer/CityNameLabel").Text = cityName;
    }
}
