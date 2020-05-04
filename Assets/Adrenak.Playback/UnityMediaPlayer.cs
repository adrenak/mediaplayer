using System;
using UnityEngine;
using UnityEngine.Video;

namespace Adrenak.MediaPlayer {
    public class UnityMediaPlayer : MonoBehaviour, IMediaPlayer {
        [SerializeField] VideoPlayer player;

        // EVENTS
        public event Action OnReady;
        public event Action OnPlay;
        public event Action OnPause;
        public event Action<long> OnSeek;
        public event Action<int> OnJump;

        // GETTERS
        public Texture Texture {
            get {
                if (player == null || player.texture == null)
                    return null;
                return player.texture;
            }
        }

        public bool IsReady { get; private set; }

        public bool IsPlaying {
            get {
                if (player == null || player.texture == null)
                    return false;
                return player.isPlaying;
            }
        }

        public long TotalFrames {
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
                return TimeSpan.FromSeconds(TotalFrames / FrameRate);
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

        bool m_AutoPlay;

        public void Open(string path, bool autoPlay) {
            this.m_AutoPlay = autoPlay;

            player.source = VideoSource.Url;
            player.url = path;
            player.Prepare();

            player.prepareCompleted -= OnPrepared;
            player.prepareCompleted += OnPrepared;
        }

        private void OnPrepared(VideoPlayer source) {
            IsReady = true;
            OnReady?.Invoke();

            if (m_AutoPlay)
                player.Play();
        }

        public void Play() {
            if (player != null && player.isPrepared) {
                OnPlay?.Invoke();
                player.Play();
            }
        }

        public void Pause() {
            if (player != null) {
                OnPause?.Invoke();
                player.Pause();
            }
        }

        public void JumpFrames(long frameDelta) {
            if (frameDelta == 0)
                return;

            var nextFrame = CurrentFrame + frameDelta;
            OnJump?.Invoke(frameDelta > 0 ? 1 : 0);
            SeekFrame(nextFrame);
        }

        public void JumpTimeSpan(TimeSpan timeSpanDelta) {
            JumpFrames((long)(timeSpanDelta.Seconds * FrameRate));
        }

        public void JumpPosition(float positionDelta) {
            var delta = positionDelta * TotalFrames;
            JumpFrames((long)delta);
        }

        public void SeekFrame(long frame) {
            if (frame < 0) {
                Debug.LogError("You're trying to seek the video to less than 0 frames");
                return;
            }

            if (frame > TotalFrames) {
                Debug.LogError("You're trying to seek the video beyond it's frames");
                return;
            }

            OnSeek?.Invoke(frame);
            player.frame = frame;
        }

        public void SeekTimeSpan(TimeSpan timeSpan) {
            var frame = timeSpan.TotalSeconds * FrameRate;
            SeekFrame((long)frame);
        }

        public void SeekPosition(float position) {
            var frame = position * TotalFrames;
            SeekFrame((long)frame);
        }
    }
}
