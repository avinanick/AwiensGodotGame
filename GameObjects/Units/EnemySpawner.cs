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

    [Signal]
    public delegate void AlienSpawned(Destructible newAlien);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        GD.Randomize();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    protected void CheckForEnhancements() {
        CampaignTracker tracker = GetNode<CampaignTracker>("/root/CampaignTrackerAL");
        int swarmingCount = tracker.CheckForEnhancement("Swarming");
        if(swarmingCount > 0) {
            SpawnPeriod = (float)(SpawnPeriod * (Mathf.Pow(0.8f,swarmingCount)));
        }
    }

    public void EndLevel() {
        GetNode<Timer>("Timer").Stop();
    }

    public void OnStartTransition() {

    }

    public void RandomizeSpawn() {

    }

    public void SetSpawn(string enemyName) {
        AlienData alienData = GetNode<AlienData>("/root/AlienDataAL");
        PackedScene newEnemyScene = (PackedScene)GD.Load(alienData.GetAlienFilePath(enemyName));
        BaseSpawnPeriod = alienData.GetAlienSpawnInterval(enemyName);
        EnemyScene = newEnemyScene;
        UpdateDifficulty();
        GetNode<Timer>("Timer").Start(SpawnPeriod);
    }

    public void SpawnEnemy() {
        float spawnAngleRadians = (float)GD.RandRange(0, 2 * Mathf.Pi);
        float xValueSpawn = Mathf.Cos(spawnAngleRadians) * SpawnRadius;
        float zValueSpawn = Mathf.Sin(spawnAngleRadians) * SpawnRadius;
        Ship newEnemy = (Ship)EnemyScene.Instance();
        GetTree().CurrentScene.AddChild(newEnemy);
        newEnemy.Translation  = new Vector3(xValueSpawn, GlobalTransform.origin.y, zValueSpawn);
        newEnemy.SpawnShip();
        EmitSignal(nameof(AlienSpawned), newEnemy);
    }

    private void UpdateDifficulty() {
        SpawnPeriod = BaseSpawnPeriod / (Mathf.Pow(1.1f, 
            GetNode<CampaignTracker>("/root/CampaignTrackerAL").GetCurrentDifficulty() - 1));
        CheckForEnhancements();
    }
}
