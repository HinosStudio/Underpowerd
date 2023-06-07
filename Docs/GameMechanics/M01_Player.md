# Player mechanic

- The player can move a character
- The player can make a character target a direction
- The player can dash
- The player can use a melee attack
- The player can interact with objects

- The player can cycle modes
- The player can go to the previous mode
- The player can use a skill
- The player can use a ranged attack

- The player has lives.
- If the player dies, he will respawn and lose a life.
- If the player dies, and he has no lives remaining, it is game over.

```C#

public class PlayerController : Monobehaviour
    [SerializeField] private Character _character;
    [SerializeField] private Weapon _weapon

    private Vector3 _Velocity;
    private CharacterController _characterController;

    private int lives;
    event outOfLivesEvent;

    // Player is flammable when true and does not have a abbility that negates burning 
    public bool flammable;

    private void Awake(){
        _characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        ApplyGravity();
        ApplyMove();
    }

    public void AddLive(int amount = 1) { }

    public void RemoveLive(int amount = 1) { }

    public void Move(){
        _velocity = _characterController.velocity;

        var desiredVelocity = input.moveDirection * maxSpeed;
        var maxSpeedChange = acceleration * Time.deltaTime;

        _velocity.x = Mathf.MoveTowards(_velocity.x, , maxSpeedChange);
        _velocity.z = Mathf.MoveTowards(_velocity.z, , maxSpeedChange);
        _velocity.y += Physics.gravity * Time.deltaTime;

        _characterController.Move(_velocity * Time.deltaTime);
    }

    public void Die(string message) {
        if(lives > 0){
            // Respawn player
        }
        else {
            // Game over
        }
    }
}

```