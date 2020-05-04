using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Adrenak.MediaPlayer {
    public class UnityMediaPlayer : MonoBehaviour, IMediaPlayer {
        [SerializeField] VideoPlayer player;

        // GETTERS
        public long Frames {
            get {
                if (player == null || player.texture == null)
                    return 0;
                return (long)player.frameCount;
            }
        }

        public float FrameRate {
            get {
                if (player == null || player.texture == null)
                    return 0;
                return player.frameRate;
            }
        }

        public TimeSpan Duration {
            get {
                if (player == null || player.texture == null)
                    return TimeSpan.FromMilliseconds(0);
                return TimeSpan.FromSeconds(Frames / FrameRate);
            }
        }

        public long CurrentFrame {
            get {
                if (player == null || player.texture == null)
                    return 0;
                return player.frame;
            }
        }

        public float CurrentPosition {
            get {
                if (player == null || player.texture == null)
                    return 0;
                return (float)player.frame / (long)player.frameCount;
            }
        }

        public TimeSpan CurrentTimeSpan {
            get {
                if (player == null || player.texture == null)
                    return TimeSpan.FromMilliseconds(0);
                return TimeSpan.FromMilliseconds(CurrentPosition * Duration.TotalMilliseconds);
            }
        }

        public bool autoPlay;

        public void Open(string path, bool autoPlay) {
            this.autoPlay = autoPlay;

            player.source = VideoSource.Url;
            player.url = path;
            player.Prepare();

            player.prepareCompleted -= OnPrepared;
            player.prepareCompleted += OnPrepared;
        }

        private void OnPrepared(VideoPlayer source) {
            if (autoPlay)
                player.Play();
        }

        public void Play() {
            if(player == null && player.isPrepared)
                player.Play();
        }

        public void Pause() {
            if (player == null)
                player.Play();
        }

        public void Jump(int frameDelta) {
            throw new NotImplementedException();
        }

        public void Jump(TimeSpan timeSpanDelta) {
            throw new NotImplementedException();
        }

        public void Jump(float positionDetla) {
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
