using System;
using Godot;

public partial class Enemy : CharacterBody2D
{
	#region Fields

	[Export] private float movementSpeed;
	[Export] private Node2D movementTarget;
	[Export] private NavigationAgent2D navigationAgent;

	#endregion

	#region Signals

	public override void _Ready()
	{
		navigationAgent.PathDesiredDistance = 4;
		navigationAgent.TargetDesiredDistance = 4;

		CallDeferred("ActorSetup");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (navigationAgent.IsNavigationFinished()) return;

		SetNextAgentPosition();
		MoveAndSlide();
	}

	#endregion

	#region Methods

	private async void ActorSetup()
	{
		await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);

		SetMovementTarget(movementTarget.Position);
	}

	private void SetMovementTarget(Vector2 movementTargetPosition)
	{
		navigationAgent.TargetPosition = movementTargetPosition;
	}

	private void SetNextAgentPosition()
	{
		var currentAgentPosition = GlobalPosition;
		var nextPathPosition = navigationAgent.GetNextPathPosition();
		var newVelocity = (nextPathPosition - currentAgentPosition).Normalized() * movementSpeed;

		Velocity = newVelocity;
	}

	#endregion
}
