using Godot;
using System;

public class CampaignTracker : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public enum GameMode {
        RLikeCampaign,
        RLiteCampaign,
        Survival
    }

    public GameMode CurrentMode = GameMode.RLikeCampaign;
    private int PopulationSaved = 0;
    private int CurrentPoints = 0;
    private int CurrentDifficultyLevel = 1;
    private int LevelPointsEarned = 0;
    private string[] LevelEnemies = new string[1] {"Alien Missile"};
    // I think I need something to track completed zones in the campaigns somehow

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

    public Godot.Collections.Dictionary<string, object> GetSaveData() {
        return new Godot.Collections.Dictionary<string, object>() {
            {"NodeType", "CampaignTracker"},
            {"CurrentMode", CurrentMode},
            {"Difficulty", CurrentDifficultyLevel}
        };
    }

    public void ConfirmLevelPoints() {
        CurrentPoints += LevelPointsEarned;
        PopulationSaved += LevelPointsEarned;
        LevelPointsEarned = 0;
    }

    public int GetCurrentDifficulty() {
        return CurrentDifficultyLevel;
    }

    public int GetCurrentPoints() {
        return CurrentPoints;
    }

    public int GetPoinsEarned() {
        return LevelPointsEarned;
    }

    public void IncrimentDifficulty() {
        CurrentDifficultyLevel += 1;
    }

    public string[] GetCurrentEnemies() {
        return LevelEnemies;
    }

    public void SetLevelEnemies(string[] nextLevelEnemies) {
        LevelEnemies = nextLevelEnemies;
    }

    public void SetLevelPoints(int levelPoints) {
        LevelPointsEarned = levelPoints;
    }
}
