using Godot;

public partial class Projectile : Area2D
{
    [Export]
    public float Speed = 600f;

    [Export]
    public float Lifetime = 3f;

    public Vector2 Direction = Vector2.Down;

    public enum ProjectileOwner
    {
        Player,
        Enemy
    }

    public ProjectileOwner BulletOwner;
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
        // Enemy bullet hits player
        if (BulletOwner == ProjectileOwner.Enemy &&
            area.IsInGroup("Player"))
        {
            Player player = area as Player;

            if (player != null)
                player.TakeDamage(1);

            QueueFree();
        }

        // Player bullet hits enemy
        if (BulletOwner == ProjectileOwner.Player &&
            area.IsInGroup("Enemy"))
        {
             var mob = area.GetParent() as Mob;

            if (mob != null)
                mob.TakeDamage(1);

            QueueFree();
        }
    }
}
