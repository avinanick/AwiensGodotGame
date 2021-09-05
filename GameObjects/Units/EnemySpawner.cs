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
    private Random RNG;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        RNG = new Random();
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
        float spawnAngleRadians = (float)RNG.NextDouble() * 2 * Mathf.Pi;
        float xValueSpawn = Mathf.Cos(spawnAngleRadians) * SpawnRadius;
        float zValueSpawn = Mathf.Sin(spawnAngleRadians) * SpawnRadius;
        Ship newEnemy = (Ship)EnemyScene.Instance();
        GetTree().CurrentScene.AddChild(newEnemy);
        newEnemy.Translation  = new Vector3(xValueSpawn, GlobalTransform.origin.y, zValueSpawn);
        newEnemy.SpawnShip();
    }

    private void UpdateDifficulty() {

    }
}
