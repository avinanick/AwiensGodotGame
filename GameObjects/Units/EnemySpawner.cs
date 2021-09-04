using Godot;
using System;

public class EnemySpawner : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    private float SpawnRadius = 40f;
    [Export]
    private float BaseSpawnPeriod = 2.0f;
    [Export]
    public int LevelDifficulty = 1;

    private float SpawnPeriod = 2.0f;
    private bool Spawning = true;
    private PackedScene EnemyScene;

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

    public void EndLevel() {
        GetNode<Timer>("Timer").Stop();
    }

    public void OnStartLevel() {
        UpdateDifficulty();
        GetNode<Timer>("Timer").Start(SpawnPeriod);
    }

    public void OnStartTransition() {

    }

    public void RandomizeSpawn() {

    }

    public void SetSpawn(PackedScene newEnemyScene) {
        EnemyScene = newEnemyScene;
    }

    public void SpawnEnemy() {

    }

    private void UpdateDifficulty() {

    }
}
