using Godot;
using System;

public class ItemSlotContainer : MarginContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Signal]
    public delegate void MouseOver();
    private bool MouseWasInsideAlready = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        base._Process(delta);
        if(GetGlobalRect().HasPoint(GetGlobalMousePosition())) {
            if(!MouseWasInsideAlready) {
                // nothing!
                MouseWasInsideAlready = true;
                EmitSignal("MouseOver");
            }
        }
        else {
            MouseWasInsideAlready = false;
        }
    }
}
