using Godot;
using System;

public class RadarIndicator : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Texture AllyIndicatorTexture = (Texture)GD.Load("res://Resources/Interfaces/dotGreen.png");
    private Color AllyModulate = new Color(1, 1, 1);
    private Texture EnemyIndicatorTexture = (Texture)GD.Load("res://Resources/Interfaces/dotRed.png");
    private Color EnemyModulate = new Color(1, 0, 0);

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

    private void AssignUnit(Destructible newUnit) {
        if(newUnit.IsInGroup("Alien")) {
            this.Texture = EnemyIndicatorTexture;
            this.Modulate = EnemyModulate;
        }
        else {
            this.Texture = AllyIndicatorTexture;
            this.Modulate = AllyModulate;
        }
        newUnit.Connect("Destroyed", this, "UnitDefeated");
        UpdatePosition(); // Probably don't need this?
    }

    private void UnitDefeated() {
        QueueFree();
    }

    private void UpdatePosition() {

    }
}
