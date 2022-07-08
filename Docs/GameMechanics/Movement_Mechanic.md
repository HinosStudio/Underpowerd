# Movement mechanics

- object will move around in world space

- object will need to move up slopes and stairs
- object will need to interact with moving platforms

```C#

public interface IMovingCharacter {
    float speed { get; }
    float direction { get; }
}

[RequireComponent(typeof(Character), typeof(CharacterController))]
public class KinematicCharacterMovement : Monobehaviour {
    private Vector3 _velocity;
    private Vector3 _desiredVelocity;

    private CharacterController _characterController;

    private void Awake(){
        // Get components
    }

    private void Update(){

    }

    public void Move() {
        
    }

    public void ApplyGravity();
}



```