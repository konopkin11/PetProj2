using UnityEngine;

namespace Character.States
{
    public class StateJumpin : StateMoving
    {
        private bool grounded;
        public StateJumpin(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Jump();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            grounded = (Character.CheckCollisionOverlap(Character.transform.position));
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (grounded) StateMachine.ChangeState(Character.StateStanding);
        }

        void Jump()
        {
            //Character.transform.Translate(Vector3.up * (Character.CollisionOverlapRadius));
            Character.ApplyImpulse(Character.JumpForce *Vector2.up);
        }
    }
}
