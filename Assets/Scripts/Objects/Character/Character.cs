using UnityEngine;
using System;

namespace RhythmGame
{
    public sealed class Character : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 2.5f;

        public void Move(Vector3 direction, float deltaTime)
        {
            transform.position += direction * (deltaTime * _speed);
        }

        public Vector3 GetPosition()
        {
            return this.transform.position;
        }
    }
}
