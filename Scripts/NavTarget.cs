using Godot;

public partial class NavTarget : Marker2D
{
	#region Fields

	[Export] private NavigationRegion2D navigationRegion;

	#endregion

	#region Signals

	[Signal] public delegate void NavTargetMovedEventHandler();

	public override void _Input(InputEvent inputEvent)
	{
		if (!IsLeftMouseClick(inputEvent)) return;

		var mouseButtonInput = inputEvent as InputEventMouseButton;
		MoveNavTarget(mouseButtonInput.Position);
	}

	#endregion

	#region Methods

	private bool IsLeftMouseClick(InputEvent inputEvent)
	{
		return inputEvent is InputEventMouseButton mouseButtonInput
				&& mouseButtonInput.ButtonIndex == MouseButton.Left
				&& mouseButtonInput.Pressed;
	}

	private void MoveNavTarget(Vector2 position)
	{
		Position = position;
		EmitSignal(SignalName.NavTargetMoved);
	}

	#endregion
}
