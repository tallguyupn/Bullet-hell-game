using Godot;
using System.Threading.Tasks;

public partial class Mob : RigidBody2D
{
    [Export] private PackedScene ProjectileScene;
    [Export] private Marker2D FirePoint;
    [Export] private AnimatedSprite2D _sprite;
    [Export] public float MoveSpeed = 100f;

    private float direction = 1f;
    private float moveTimer = 0f;
    private float shootTimer = 0f;

    private RandomNumberGenerator rng = new();
    private bool _attacking = false;
    public override void _Ready()
    {
        rng.Randomize();

        // Initial random timers
        moveTimer = rng.RandfRange(1f, 3f);
        shootTimer = rng.RandfRange(0.5f, 2f);
         _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        _sprite.AnimationFinished += OnAnimationFinished;
    }

     public override void _PhysicsProcess(double delta)
    {
        float d = (float)delta;

        // Move left/right
        moveTimer -= d;

        if (moveTimer <= 0)
        {
            direction *= -1; // Switch direction
            moveTimer = rng.RandfRange(1f, 3f);
        }

        LinearVelocity = new Vector2(direction * 100f, 0);
        // Random shooting
        shootTimer -= d;

        if (shootTimer <= 0)
        {
            Shoot();
            shootTimer = rng.RandfRange(0.5f, 2f);
        }
    }
    public async Task Attack()
    {
        if (_attacking)
            return;

        _attacking = true;


        for (int i = 0; i < 10; i++)
        {

            await ToSignal(
                GetTree().CreateTimer(0.2f),
                SceneTreeTimer.SignalName.Timeout
            );
        }

        _attacking = false;
    }

    public override void _Process(double delta)
    {

    }
    private void Shoot()
    {
        if (ProjectileScene == null || FirePoint == null)
        return;

        _sprite.Play("shoot");

        var projectile = ProjectileScene.Instantiate<Projectile>();

        projectile.GlobalPosition = FirePoint.GlobalPosition;
        projectile.Direction = Vector2.Down;

        GetTree().CurrentScene.AddChild(projectile);
    } 
    private void OnAnimationFinished()
    {
     if (_sprite.Animation == "shoot")
         {
         _sprite.Play("idle");
        }
    }

}
