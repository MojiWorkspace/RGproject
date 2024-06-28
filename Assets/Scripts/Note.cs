using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Note : MonoBehaviour
{
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

        // если нота длинная - вытягиваем и смещаем точку спавна/деспавна по Y
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
        if (transform.localPosition.y < noteDespawnY) Destroy(gameObject);

        transform.Translate(Vector3.down * 30 * Time.deltaTime); //значение скорости передвижения захардкожена на данный момент (NoteSpawnY / NoteTime)

        GetComponent<SpriteRenderer>().enabled = true;
    }
}
