using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Note : MonoBehaviour
{
    //double timeInstantiated;
    public float assignedTime;
    public float assignedLength;
    private float noteSpawnY;
    private float noteTapY;
    private float noteDespawnY;
    void Start()
    {
        noteSpawnY = SongManager.Instance.noteSpawnY;
        noteTapY = SongManager.Instance.noteTapY;
        noteDespawnY = noteTapY - noteSpawnY;
        //timeInstantiated = SongManager.GetAudioSourceTime();

        if (assignedLength > 1) {
            Vector3 baseScale = transform.localScale;
            baseScale.y *= assignedLength*2;
            transform.localScale = baseScale;

            noteSpawnY += baseScale.y/2;
            noteDespawnY -= baseScale.y/2;

        }
        transform.localPosition = Vector3.up * noteSpawnY;
    }

    void Update()
    {
        // double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        if (transform.localPosition.y < noteDespawnY)
        {
            Destroy(gameObject);
        }
            transform.Translate(Vector3.down * 30 * Time.deltaTime); //скорость передвижения захардкожена на данный момент
            GetComponent<SpriteRenderer>().enabled = true;
    }
}
