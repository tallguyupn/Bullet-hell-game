using Godot;
using System.Threading.Tasks;

public partial class Mob : RigidBody2D
{
    [Export] private PackedScene ProjectileScene;
    [Export] private Marker2D FirePoint;
    [Export] private AnimatedSprite2D _sprite;
    private bool _attacking = false;
    public override void _Ready()
    {
         _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        _sprite.AnimationFinished += OnAnimationFinished;
    }
    public async Task Attack()
    {
        if (_attacking)
            return;

        _attacking = true;


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
        _sprite.Play("shoot");
       var projectile = ProjectileScene.Instantiate<Projectile>();

        projectile.GlobalPosition = FirePoint.GlobalPosition;
        projectile.Direction = Vector2.Down;

        GetTree().CurrentScene.AddChild(projectile);
    }
    public override void _Process(double delta)
    {
     if (Input.IsActionJustPressed("ui_accept"))
     {
        GD.Print("shooting");
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
    private void OnAnimationFinished()
    {
     if (_sprite.Animation == "shoot")
         {
         _sprite.Play("idle");
        }
    }

}
