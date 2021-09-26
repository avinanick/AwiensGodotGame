using Godot;
using System;

public class OverworldOverlay : MarginContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        CallDeferred(nameof(UpdateStats));
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void ItemsClicked() {

    }

    public void QuitClicked() {

    }

    public void ResearchClicked() {

    }

    private void UpdateStats() {
        // Update points label
        CampaignTracker tracker = GetNode<CampaignTracker>("/root/CampaignTrackerAL");
        int credits = tracker.GetCurrentPoints();
        string creditsText = "Credits: " +  credits.ToString();
        GetNode<Label>("VBoxContainer/PanelContainer/HBoxContainer/PointsLabel").Text = creditsText;
        // Update progress label
    }
}
