using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Game Data/Character Data")]
    public class CharacterData : ScriptableObject
    {
        public float movementSpeed = 200f;
        public float normalColliderHeight = 3f;
        public float jumpForce = 10f;
        public float collisionOverlapRadius = 0.1f;
    }
}
