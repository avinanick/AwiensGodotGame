using Godot;
using System;

public class ThreatIcon : MarginContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    private int MaxSizeX = 64;
    [Export]
    private int MaxSizeY = 64;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void SetIcon(Texture iconImage) {
        GetNode<TextureRect>("PanelContainer/TextureRect").Texture = iconImage;
        Hide();
        Show();
        Vector2 scaling = new Vector2(1,1);
        if(RectSize.x > MaxSizeX) {
            scaling.x = MaxSizeX / RectSize.x;
        }
        if(RectSize.y > MaxSizeY) {
            scaling.y = MaxSizeY / RectSize.y;
        }
        RectScale = scaling;
        GD.Print(scaling);
    }
}
