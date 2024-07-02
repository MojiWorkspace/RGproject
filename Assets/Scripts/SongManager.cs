using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

namespace RhythmGame {
    public class SongManager : MonoBehaviour, IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        public static SongManager Instance;
        public AudioSource audioSource;
        public Lane[] lanes;
        public float songDelayInSeconds;
        public double marginOfError; // в секундах
        public int inputDelayInMilliseconds;
        public string fileLocation;
        public float noteTime;
        public float noteSpawnY;
        public float noteTapY;
        public static MidiFile midiFile;

        private void Start() => IGameListener.Register(this);
        private void ReadFromFile()
        {
            midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
            GetDataFromMidi();
        }

        public static double GetAudioSourceTime()
        {
            return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
        }
        public void OnStartGame()
        {
            Instance = this;
            ReadFromFile();
        }
        public void StartSong() => audioSource.Play();
        public void OnPauseGame() => audioSource.Pause();
        public void OnResumeGame() => audioSource.Play();
        public void OnFinishGame() => audioSource.Stop();
        public void GetDataFromMidi()
        {
            var notes = midiFile.GetNotes();
            var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];

            notes.CopyTo(array, 0);

            foreach (var lane in lanes) lane.SetTimeStamps(array);

            Invoke(nameof(StartSong), songDelayInSeconds);
        }
    }
}