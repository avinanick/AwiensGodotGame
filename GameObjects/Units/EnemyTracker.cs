using Godot;
using System;

public class EnemyTracker : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private PackedScene SpawnerScene = (PackedScene)GD.Load("res://GameObjects/Units/EnemySpawner.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        // This should ask the Campaign manager about the current enemy requirements, create
        // the needed spawners, and order them to start
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void SetLevelEnemies() {
        
    }
}
