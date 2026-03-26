using UnityEngine;
public class BossStateMachine : FSM {

    [HideInInspector] public Animator animator;
        
    protected override void Start() {
        _states["idle"] = new IdleState();
        _states["charge"] = new ChargeState();
        _states["spit"] = new SpitState();
        _states["summon"] = new SummonState();
        _states["beam"] = new BeamState();
        _currentState = "idle";
        base.Start();

        animator = GetComponent<Animator>();
    }

}
