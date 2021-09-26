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

    private void CloseMenus() {
        Godot.Collections.Array<Control> menus = new Godot.Collections.Array<Control>(GetNode<Node>("VBoxContainer/MarginContainer").GetChildren());
        for(int i = 0; i < menus.Count; i++) {
            menus[i].Visible = false;
        }
    }

    public void ItemsClicked() {
        CloseMenus();
        GetNode<Control>("VBoxContainer/MarginContainer/ItemsInterface").Visible = true;
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
