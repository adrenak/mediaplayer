using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Adrenak.MediaPlayer{
    public class UnityMediaPlayer : MonoBehaviour, IMediaPlayer {
        [SerializeField] VideoPlayer player;

        public int CurrentFrame => throw new NotImplementedException();

        public float CurrentPosition => throw new NotImplementedException();

        public TimeSpan CurrentTimeSpan => throw new NotImplementedException();

        public void Jump(int frameDelta) {
            throw new NotImplementedException();
        }

        public void Jump(TimeSpan timeSpanDelta) {
            throw new NotImplementedException();
        }

        public void Jump(float positionDetla) {
            throw new NotImplementedException();
        }

        public void Open(string path, bool autoPlay) {
            throw new NotImplementedException();
        }

        public void Pause() {
            throw new NotImplementedException();
        }

        public void Play() {
            throw new NotImplementedException();
        }

        public void Seek(int frame) {
            throw new NotImplementedException();
        }

        public void Seek(TimeSpan timeSpace) {
            throw new NotImplementedException();
        }

        public void Seek(float position) {
            throw new NotImplementedException();
        }
    }
}
