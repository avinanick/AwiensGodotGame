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
    private string[] LevelEnemyEnhancements = new string[] {};
    // I think I need something to track completed zones in the campaigns somehow
    private int CurrentCity = -1;
    private System.Collections.Generic.Stack<int> CompletedCities = new System.Collections.Generic.Stack<int>();

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
            {"Difficulty", CurrentDifficultyLevel},
            {"CompletedCities", CompletedCities.ToArray()},
            {"PopulationSaved", PopulationSaved},
            {"CurrentPoints", CurrentPoints}
        };
    }

    public bool CheckForEnhancement(String enhancement) {
        if(Array.Exists<String>(LevelEnemyEnhancements, element => element == "Shielded")) {
            return true;
        }
        return false;
    }

    public void ConfirmLevelPoints() {
        CurrentPoints += LevelPointsEarned;
        PopulationSaved += LevelPointsEarned;
        LevelPointsEarned = 0;
        CompletedCities.Push(CurrentCity);
        CurrentCity = -1;
    }

    public System.Collections.Generic.Stack<int> GetCitiesCompleted() {
        return CompletedCities;
    }

    public int GetCurrentDifficulty() {
        return CurrentDifficultyLevel;
    }

    public string[] GetCurrentEnemyEnhancements() {
        return LevelEnemyEnhancements;
    }

    public int GetCurrentPoints() {
        return CurrentPoints;
    }

    public int GetNumCitiesComplete() {
        return CompletedCities.Count;
    }

    public int GetPointsEarned() {
        return LevelPointsEarned;
    }

    public void IncrimentDifficulty() {
        CurrentDifficultyLevel += 1;
    }

    public string[] GetCurrentEnemies() {
        return LevelEnemies;
    }

    public void SetLevelEnemies(string[] nextLevelEnemies, int nextCity) {
        LevelEnemies = nextLevelEnemies;
        CurrentCity = nextCity;
    }

    public void SetLevelEnemyEnhancements(string[] nextLevelEnhancements) {
        LevelEnemyEnhancements = nextLevelEnhancements;
    }

    public void SetLevelPoints(int levelPoints) {
        LevelPointsEarned = levelPoints;
    }
}
