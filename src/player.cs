using Godot;
using System.Threading.Tasks;
public partial class Player : Area2D {
	[Export] public int Speed { get; set; } = 400; //Speed of the player in pixels per second
	[Export] public int MaxHealth = 3;
	
	public Vector2 ScreenSize; //Size of the game window

	private int _health;
    private Label _healthLabel;

	public override void _Ready() {
	   ScreenSize = GetViewportRect().Size;
	    _health = MaxHealth;

		_healthLabel = GetNode<Label>("../CanvasLayer/HealthLabel");  
		UpdateHealthUI();
	}

	public override void _Process(double delta) {
 
 	   var velocity = Vector2.Zero; // The player's movement vector.

	    if (Input.IsActionPressed("move_right")) {
 	       velocity.X += 1;
	  	}

	    if (Input.IsActionPressed("move_left")) {
 	       velocity.X -= 1;
	  	}

 	   if (Input.IsActionPressed("move_down")) {
  	      velocity.Y += 1;
		}

 	   if (Input.IsActionPressed("move_up")) {
  	      velocity.Y -= 1;
	    }

	    var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

    	if (velocity.Length() > 0) {
     	   velocity = velocity.Normalized() * Speed;
      	  animatedSprite2D.Play();
    	}
    
		else {
  	      animatedSprite2D.Stop();
   	 }
		Position += velocity * (float)delta;
		Position = new Vector2(
  	  		x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
 	   		y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
	}
	  private void UpdateHealthUI()
    {
        _healthLabel.Text = $"Health: {_health}";
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        UpdateHealthUI();

        GD.Print($"Player health: {_health}");

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GD.Print("Player died!");

        GetTree().ReloadCurrentScene();
    }
}

