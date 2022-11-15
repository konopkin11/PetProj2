using System;
using Character.States;
using UnityEngine;

namespace Character
{
    public class Character : MonoBehaviour
    {
        #region StateMachingMoving
        public StateMachine StateMachine;

        public StateStanding StateStanding;

        public StateJumpin StateJumping;
        #endregion

        [SerializeField]
        private CharacterData _data;
        public float JumpForce => _data.jumpForce;
        public float MovementSpeed => _data.movementSpeed;
        public float CollisionOverlapRadius => _data.collisionOverlapRadius;
        [SerializeField] private LayerMask whatIsFloor;
        
        void Start()
        {
            StateMachine = new StateMachine();
            StateStanding = new StateStanding(this, StateMachine);
            StateJumping = new StateJumpin(this, StateMachine);
            StateMachine.Initialize(StateStanding);

        }

        public void Move(float speed)
        {
            Vector2 targetVelocity = new Vector2(speed * MovementSpeed * Time.deltaTime, 0);
            GetComponent<Rigidbody2D>().velocity = targetVelocity;
        }
        public bool CheckCollisionOverlap(Vector2 point)
        {
            
            return Physics2D.OverlapCircleAll(point, CollisionOverlapRadius, whatIsFloor).Length>0;
        }
        
        
        

        public void ApplyImpulse(Vector2 force)
        {
            GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

        }
        void Update()
        {
            StateMachine.CurrentState.HandleInput();
            StateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }
    }
}
