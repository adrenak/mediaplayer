using System;
using UnityEngine;
using UnityEngine.Video;

namespace Adrenak.MediaPlayer {
    public class UnityMediaPlayer : MonoBehaviour, IMediaPlayer {
        [SerializeField] VideoPlayer player;

        // EVENTS
        public event Action<string> OnOpen;
        public event Action OnReady;
        public event Action<Exception> OnError;
        public event Action OnPlay;
        public event Action OnPause;
        public event Action OnStop;
        public event Action OnSeek;
        public event Action OnJump;

        // GETTERS
        public Texture MediaTexture {
            get {
                if (player == null || player.texture == null)
                    return null;
                return player.texture;
            }
        }

        public bool IsReady => player.isPrepared;

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

        public void Open(string path, bool autoPlay) {
            player.source = VideoSource.Url;
            player.url = path;
            player.Prepare();

            void OnPrepared(VideoPlayer player) {
                OnReady?.Invoke();

                if (autoPlay)
                    Play();
            }

            void OnErrorReceived(VideoPlayer player, string message){
                OnError?.Invoke(new Exception(message));
            }

            player.prepareCompleted -= OnPrepared;
            player.prepareCompleted += OnPrepared;

            player.errorReceived -= OnErrorReceived;
            player.errorReceived += OnErrorReceived;

            OnOpen?.Invoke(path);
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

        public void Stop() {
            if(player != null){
                player.Stop();
                OnStop?.Invoke();
            }
        }

        public void SeekFrame(long frame) {
            if (MediaTexture == null)
                return;

            if (frame < 0)
                frame = 0;

            if (frame > TotalFrames)
                frame = TotalFrames;

            OnSeek?.Invoke();
            player.frame = frame;
        }

        public void SeekTimeSpan(TimeSpan timeSpan) {
            var frame = timeSpan.TotalSeconds * FrameRate;
            SeekFrame((long)frame);
        }

        public void SeekPosition(float position) {
            position = Mathf.Clamp01(position);
            var frame = position * TotalFrames;
            SeekFrame((long)frame);
        }

        public void JumpFrames(long frameDelta) {
            if (MediaTexture == null || frameDelta == 0)
                return;

            var nextFrame = CurrentFrame + frameDelta;
            OnJump?.Invoke();
            SeekFrame(nextFrame);
        }

        public void JumpTimeSpan(TimeSpan timeSpanDelta) {
            JumpFrames((long)(timeSpanDelta.Seconds * FrameRate));
        }

        public void JumpPosition(float positionDelta) {
            var delta = positionDelta * TotalFrames;
            JumpFrames((long)delta);
        }
    }
}
