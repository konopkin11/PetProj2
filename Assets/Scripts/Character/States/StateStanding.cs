using UnityEngine;

namespace Character.States
{
    public class StateStanding : StateGrounded
    {
        private bool jump;

        public StateStanding(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void HandleInput()
        {
            base.HandleInput();
            jump = Input.GetKey(KeyCode.Space);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (jump)
            {
                StateMachine.ChangeState(Character.StateJumping);
            }
        }
    }
}
