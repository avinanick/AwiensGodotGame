using Godot;
using System;

public class ResearchTreeNode : Button
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Button[] Neighbors;
    private Line2D[] Lines;
    private Line2D[] NeighborLines;
    private float Delay = 10f;

    [Export]
    private int Cost = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
