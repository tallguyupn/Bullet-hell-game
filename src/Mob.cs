using Godot;
using System.Threading.Tasks;

public partial class Mob : RigidBody2D
{
    [Export] private PackedScene ProjectileScene;
    [Export] private Marker2D FirePoint;
    [Export] private AnimationPlayer AnimationPlayer;

    private bool _attacking = false;

    public async Task Attack()
    {
        if (_attacking)
            return;

        _attacking = true;

        AnimationPlayer.Play("shoot");

        for (int i = 0; i < 10; i++)
        {
            ShootProjectile();

            await ToSignal(
                GetTree().CreateTimer(0.2f),
                SceneTreeTimer.SignalName.Timeout
            );
        }

        _attacking = false;
    }

    private void ShootProjectile()
    {
        var projectile = ProjectileScene.Instantiate<Projectile>();

        projectile.GlobalPosition = FirePoint.GlobalPosition;

        // Fire in the enemy's facing direction
        projectile.Direction = Transform.X.Normalized();

        GetTree().CurrentScene.AddChild(projectile);
    }
    public override void _Process(double delta)
    {
     if (Input.IsActionJustPressed("ui_accept"))
     {
        _=BulletHellTest();
     }
    }
    private async Task BulletHellTest()
    {
    for (int i = 0; i < 50; i++)
    {
        ShootProjectile();

        await ToSignal(
            GetTree().CreateTimer(0.05f),
            SceneTreeTimer.SignalName.Timeout
        );
    }

    }
}
