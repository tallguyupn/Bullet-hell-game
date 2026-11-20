using Godot;
using System;

public partial class Main : Node2D
{
	private bool canMove = false;
	// Called when the node enters the scene tree for the first time.
	public async override void _Ready()
	{
		var player = GetNode<Player>("Player");
		var mob = GetNode<Mob>("Mob");
		var audio = GetNode<AudioStreamPlayer>("CanvasLayer/Get_ready");
		var anim = GetNode<AnimationPlayer>("CanvasLayer/AnimationPlayer");
		var music = GetNode<AudioStreamPlayer>("Music");

		player.CanControl = false;
		mob.CanMove = false;

		audio.Play();
 		anim.Play("get_ready");
		await ToSignal(anim, AnimationPlayer.SignalName.AnimationFinished);
		player.CanControl = true;
		mob.CanMove = true;
		music.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
