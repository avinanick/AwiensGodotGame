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
        // I hope this call deferred works, I'm iffy on the array args part
        CallDeferred("SetLevelEnemies", GetNode<CampaignTracker>("/root/CampaignTrackerAL").GetCurrentEnemies());
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void SetLevelEnemies(string[] enemyList) {
        for(int i = 0; i < enemyList.Length; i++) {
            EnemySpawner newSpawner = (EnemySpawner)SpawnerScene.Instance();
            // Maybe I'll change the arg for this method to be a packed scene array?
            newSpawner.SetSpawn(enemyList[i]);
            newSpawner.Translation = Translation;
            AddChild(newSpawner);
        }
    }
}