using Godot;
using System;

public class OverworldCities : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Vector2[] CityPositions;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Godot.Collections.Array<Node2D> cities = new Godot.Collections.Array<Node2D>(GetChildren());
        CityPositions = new Vector2[cities.Count];
        for(int i = 0; i < cities.Count; i++) {
            CityPositions[i] = cities[i].GlobalPosition;
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void CityClicked(int cityNumber) {

    }

    public void CityMouseEntered(int cityNumber) {

    }

    public void CityMouseExited(int cityNumber) {

    }

    public void LoadThreats() {

    }

    public void SaveThreats() {

    }

    public void UpdateThreats() {

    }
}
