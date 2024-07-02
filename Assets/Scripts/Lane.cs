using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RhythmGame {
    public class Lane : MonoBehaviour, IGameUpdateListener
    {
        public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
        public KeyCode input;
        public GameObject notePrefab;
        public GameObject progressBar;

        public GameObject scoreManager;
        List<Note> notes = new List<Note>();
        public List<double> timeStamps = new List<double>();
        public List<double> notesLen = new List<double>();

        int spawnIndex = 0;
        int inputIndex = 0;

        private void Start() => IGameListener.Register(this);
        private void Hit() => scoreManager.GetComponent<ScoreManager>().Hit();
        private void Miss() => scoreManager.GetComponent<ScoreManager>().Miss();

        public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
        {
            TempoMap tempoMap = SongManager.midiFile.GetTempoMap();

            foreach (var note in array)
            {
                if (note.NoteName == noteRestriction)
                {
                    var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, tempoMap);
                    var noteLength = note.LengthAs<MetricTimeSpan>(tempoMap).TotalMicroseconds / 1000 / 107;

                    timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
                    notesLen.Add(noteLength);
                }
            }
        }
        public void OnUpdate(float deltaTime)
        {
            if (spawnIndex < timeStamps.Count)
            {
                if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
                {
                    var note = Instantiate(notePrefab, transform);
                    notes.Add(note.GetComponent<Note>());
                    note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                    note.GetComponent<Note>().assignedLength = (float)notesLen[spawnIndex];

                    if (progressBar)
                    {
                        progressBar.GetComponent<ProgressBar>().isActive = true;
                        progressBar.GetComponent<ProgressBar>().isActiveAfterTime = SongManager.Instance.noteTime;
                        progressBar.GetComponent<ProgressBar>().activeTime = note.GetComponent<Note>().assignedLength * 107 / 1000;
                    }

                    spawnIndex++;
                }
            }

            if (inputIndex < timeStamps.Count)
            {
                double timeStamp = timeStamps[inputIndex];
                double marginOfError = SongManager.Instance.marginOfError;
                double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

                if (Input.GetKeyDown(input))
                {
                    if (Math.Abs(audioTime - timeStamp) < marginOfError)
                    {
                        Hit();
                        print($"Hit on {inputIndex} note");

                        if (notes[inputIndex].GetComponent<Note>().assignedLength == 1) Destroy(notes[inputIndex].gameObject);

                        inputIndex++;
                    }
                    else
                    {
                        print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                    }
                }

                if (timeStamp + marginOfError <= audioTime)
                {
                    if (!progressBar)
                    {
                        Miss();
                        print($"Missed {inputIndex} note");
                        inputIndex++;
                    }
                }
            }
        }
    }
}
