using Godot;

public partial class Projectile : Area2D
{
    [Export]
    public float Speed = 600f;

    [Export]
    public float Lifetime = 3f;

    public Vector2 Direction = Vector2.Down;

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
        // Automatically remove the projectile after a few seconds
        GetTree().CreateTimer(Lifetime).Timeout += QueueFree;
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += Direction * Speed * (float)delta;
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area.IsInGroup("Player"))
        {
            GD.Print("Player hit!");
            QueueFree();
        }
    }
	private void SpawnProjectile()
{
	GD.Print("Projectile spawned");
}
}
