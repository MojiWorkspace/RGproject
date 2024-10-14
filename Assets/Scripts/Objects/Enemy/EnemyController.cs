using System;
using UnityEngine;

namespace RhythmGame
{
    public class EnemyController : MonoBehaviour
    {
        //[SerializeField] private Collider2D _collider;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.gameObject.name);
        }

        /*
        private void OnTriggerStay2D(Collider2D other)
        {
            throw new NotImplementedException();
        }


        private void OnCollisionStay2D(Collision2D collider)
        {
            Debug.Log("Столкновение с объектом: " + collider.gameObject.name);
            
            if (collider.gameObject.tag == "Player")
            {
                Debug.Log("+++++");
            }
        }
        */
    }
}