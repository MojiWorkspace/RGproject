using System;
using UnityEngine;

namespace RhythmGame
{
    public sealed class InputManager : MonoBehaviour
    {
        [SerializeField] private MoveConfig _config;

        public Vector3 Move()
        {
            Vector3 direction = Vector3.zero;
            
            if (Input.GetKey(_config.forward))
            {
                direction.y = 1;
                //Debug.Log("direction.y = 1");
            }
            else if (Input.GetKey(_config.back))
            {
                direction.y = -1;
                //Debug.Log("direction.y = -1");
            }

            if (Input.GetKey(_config.left))
            {
                direction.x = -1;
                //Debug.Log("direction.x = -1");
            }
            else if (Input.GetKey(_config.right))
            {
                direction.x = 1;
                //Debug.Log("direction.x = 1");
            }

            return direction;
        }
    }
}