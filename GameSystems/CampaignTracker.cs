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
    // I think I need something to track completed zones in the campaigns somehow

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
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

    public int GetCurrentDifficulty() {
        return CurrentDifficultyLevel;
    }

    public void IncrimentDifficulty() {
        CurrentDifficultyLevel += 1;
    }
}
