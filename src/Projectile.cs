using Godot;

public partial class Projectile : Area2D
{
    [Export]
    public float Speed = 600f;

    [Export]
    public float Lifetime = 3f;

    public Vector2 Direction = Vector2.Right;

    public override void _Ready()
    {
        // Automatically remove the projectile after a few seconds
        GetTree().CreateTimer(Lifetime).Timeout += QueueFree;
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += Direction * Speed * (float)delta;
    }

    private void OnBodyEntered(Node2D body)
    {
        // Optional: damage player here

        QueueFree();
    }
	private void SpawnProjectile()
{
	GD.Print("Projectile spawned");
}
}
