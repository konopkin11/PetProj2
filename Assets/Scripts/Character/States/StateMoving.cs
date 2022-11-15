using Unity.VisualScripting;
using UnityEngine;

namespace Character.States
{
    public class StateMoving : State
    {
        private float horizonalInput;
        
        public StateMoving(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            horizonalInput = 0.0f;
        }
        public override void HandleInput()
        {
            base.HandleInput();
            horizonalInput = Input.GetAxis("Horizontal");
        }

       

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Character.Move(horizonalInput);
        }

        public override void Exit()
        {
            base.Exit();
        }
        
    }
}
