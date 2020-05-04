using System;

namespace Adrenak.MediaPlayer {
    public interface IMediaPlayer {
        void Open(string path, bool autoPlay);
        void Play();
        void Pause();

        void Jump(int frameDelta);
        void Jump(TimeSpan timeSpanDelta);
        void Jump(float positionDetla);

        void Seek(int frame);
        void Seek(TimeSpan timeSpace);
        void Seek(float position);

        int CurrentFrame { get; }
        float CurrentPosition { get; }
        TimeSpan CurrentTimeSpan { get; }
    }
}
