using Godot;
using System;

public class CityConfirmationInterface : MarginContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Signal]
    public delegate void Confirmed();
    [Signal]
    public delegate void Cancelled();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Visible = false;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void AcceptButtonPressed() {
        EmitSignal(nameof(Confirmed));
        Visible = false;
    }

    public void CancelButtonPressed() {
        EmitSignal(nameof(Cancelled));
        Visible = false;
    }

    public void CityClicked() {

    }
}
