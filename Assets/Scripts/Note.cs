using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    double timeInstantiated;
    public float assignedTime;
    public float assignedLength;
    private float changeY = 0;
    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();

        // Если нота с удержанием, то вычисяем изначальное смещение по Y и новый скейл по Y
        // if (assignedLength > 1) {
        //     Vector3 baseScale = transform.localScale;

        //     changeY = baseScale.y * (assignedLength - 1);
        //     baseScale.y *= assignedLength;
        //     transform.localScale = baseScale;
        // }

    }

    void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        // t параметр изменяется от 0 до 1 в Lerp()
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime *2));

        if (t > 1)
        {
            // если достигли координаты деспавна, то уничтожаем объект
            Destroy(gameObject);
        }
        else
        {
            // перемещаем ноту от точки спавна (со смещением по Y) до точки деспавна
            transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnY + new Vector3(0, changeY, 0), Vector3.up * SongManager.Instance.noteDespawnY, t);
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
