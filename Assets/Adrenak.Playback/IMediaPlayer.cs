using System;

namespace Adrenak.MediaPlayer {
    public interface IMediaPlayer {
        event Action OnReady;
        event Action OnPlay;
        event Action OnPause;

        TimeSpan Duration { get; }
        long TotalFrames { get; }
        float FrameRate { get; }
        long CurrentFrame { get; }
        float CurrentPosition { get; }
        TimeSpan CurrentTimeSpan { get; }

        void Open(string path, bool autoPlay);
        void Play();
        void Pause();

        void JumpFrames(int frameDelta);
        void JumpTimeSpan(TimeSpan timeSpanDelta);
        void JumpPosition(float positionDetla);

        void Seek(int frame);
        void Seek(TimeSpan timeSpace);
        void Seek(float position);
    }
}
