using UnityEngine;

namespace Character.States
{
    public abstract class State
    {
        protected Character Character;

        protected StateMachine StateMachine;
        // Start is called before the first frame update
   
        protected State(Character character, StateMachine stateMachine)
        {
            this.Character = character;
            this.StateMachine = stateMachine;
        }
        // Update is called once per frame
        protected void DisplayOnUI()
        {
            Debug.Log(this);
        }
        public virtual void Enter()
        {
            DisplayOnUI();
        }

        public virtual void HandleInput()
        {

        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }
}
