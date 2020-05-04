using System;

namespace Adrenak.MediaPlayer {
    public interface IMediaPlayer {
        event Action OnReady;
        event Action OnPlay;
        event Action OnPause;
        event Action<long> OnSeek;
        event Action<int> OnJump;

        bool IsReady { get; }
        bool IsPlaying { get; }
        TimeSpan Duration { get; }
        long TotalFrames { get; }
        float FrameRate { get; }
        long CurrentFrame { get; }
        float CurrentPosition { get; }
        TimeSpan CurrentTimeSpan { get; }

        void Open(string path, bool autoPlay);
        void Play();
        void Pause();

        void JumpFrames(long frameDelta);
        void JumpTimeSpan(TimeSpan timeSpanDelta);
        void JumpPosition(float positionDetla);

        void SeekFrame(long frame);
        void SeekTimeSpan(TimeSpan timeSpace);
        void SeekPosition(float position);
    }
}
