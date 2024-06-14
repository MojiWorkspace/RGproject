using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>(); // список таймингов нот в миди файле
    public List<double> notesLen = new List<double>(); // список длин нот из миди файла

    int spawnIndex = 0;
    int inputIndex = 0;

    void Start()
    {

    }
    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        TempoMap tempoMap = SongManager.midiFile.GetTempoMap();

        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, tempoMap);
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);

                //временный хардкод для получения длины ноты в виде числа (1 - обычная нота, все, что больше - нота с удержанием, потом на это число умножаем скейл по Y)
                var noteLength = note.LengthAs<MetricTimeSpan>(tempoMap).TotalMicroseconds / 1000 / 107;
                notesLen.Add(noteLength);
            }
        }
    }
    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                //спавним очередную ноту и присваиваем ей значения параметров
                var note = Instantiate(notePrefab, transform);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                note.GetComponent<Note>().assignedLength = (float)notesLen[spawnIndex];
                spawnIndex++;
            }
        }

        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            //обрабаботка ввода
            if (Input.GetKeyDown(input))
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit();
                    print($"Hit on {inputIndex} note");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else
                {
                    print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            }
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                print($"Missed {inputIndex} note");
                inputIndex++;
            }
        }

    }
    private void Hit()
    {
        ScoreManager.Hit();
    }
    private void Miss()
    {
        ScoreManager.Miss();
    }
}
