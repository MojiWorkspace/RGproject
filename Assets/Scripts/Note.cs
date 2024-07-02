using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace RhythmGame {
    public class Note : MonoBehaviour
    {
        public float assignedTime;
        public float assignedLength;

        private float noteMovementSpeed;
        private float noteSpawnY;
        private float noteTapY;
        private float noteDespawnY;

        private void Start()
        {
            noteMovementSpeed = SongManager.Instance.noteSpawnY/SongManager.Instance.noteTime;
            noteSpawnY = SongManager.Instance.noteSpawnY;
            noteTapY = SongManager.Instance.noteTapY;
            noteDespawnY = noteTapY - noteSpawnY;

            if (assignedLength > 1)
            {
                Vector3 baseScale = transform.localScale;
                baseScale.y *= assignedLength * 2;
                transform.localScale = baseScale;
                noteSpawnY += baseScale.y/2;
                noteDespawnY -= baseScale.y/2;
            }

            transform.localPosition = Vector3.up * noteSpawnY;
        }
        private void Update()
        {
            if (gameObject != null)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                transform.Translate(Vector3.down * noteMovementSpeed * Time.deltaTime);

                if (transform.localPosition.y < noteDespawnY) Destroy(gameObject);

            }
        }
    }
}
