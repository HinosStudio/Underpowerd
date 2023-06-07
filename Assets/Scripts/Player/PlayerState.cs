using hinos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class PlayerState : State<PlayerController> {
    protected readonly PlayerStateFactory factory;

    public PlayerState(PlayerController source, string name, PlayerStateFactory factory) : base(source, name) {
        this.factory = factory;
    }
}

public class PlayerStateFactory {
    private readonly IdlePlayerState idleState;
    private readonly DeadPlayerState deadState;
    
    public PlayerStateFactory(PlayerController source) {
        idleState = new IdlePlayerState(source, this);
        deadState = new DeadPlayerState(source, this);
    }

    public IdlePlayerState GetIdleState() => idleState;

    public DeadPlayerState GetDeadState() => deadState;
}

public class IdlePlayerState : PlayerState {

    public IdlePlayerState(PlayerController source, PlayerStateFactory factory) : base(source, "Idle", factory) {

    }

    public override void Enter() {
        
    }

    public override void Exit() {
        
    }

    public override void Update() {
        
    }
}

public class DeadPlayerState : PlayerState {

    public DeadPlayerState(PlayerController source, PlayerStateFactory factory) : base(source, "Idle", factory) {

    }

    public override void Enter() {

    }

    public override void Exit() {

    }

    public override void Update() {

    }
}