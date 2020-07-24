using System;
using UnityEngine;

namespace Adrenak.MediaPlayer {
    public interface IVideoPlayer {
        /// <summary>
        /// Fired when a request to open a video stream is made
        /// </summary>
        event Action<string> OnOpen;

        /// <summary>
        /// Fired when a video is loaded and it capable of being player
        /// </summary>
        event Action OnReady;

        /// <summary>
        /// Fired when there is an error in loading the video
        /// </summary>
        event Action<Exception> OnError;

        /// <summary>
        /// Fired when the video starts playing for the first time or is resumed in the middle of the playback
        /// </summary>
        event Action OnPlay;

        /// <summary>
        /// Fired when the video is paused
        /// </summary>
        event Action OnPause;

        /// <summary>
        /// Fired when the playback is stopped
        /// </summary>
        event Action OnStop;

        /// <summary>
        /// Fired when the media player seeks to another frame
        /// </summary>
        event Action OnSeek;

        /// <summary>
        /// Fired when the media played jumps forward or backward by any number of frames
        /// </summary>
        event Action OnJump;

        [Obsolete("Use VideoTexture property instead. This property will soon be removed!")]
        Texture MediaTexture { get; }

        /// <summary>
        /// The `Texture` object that represents the texture on which the video plays
        /// </summary>
        Texture VideoTexture { get; }

        /// <summary>
        /// If the video player has loaded the video and the video can be played or not
        /// </summary>
        bool IsReady { get; }

        /// <summary>
        /// If the video is presently playing. False when the video is not loaded or paused.
        /// </summary>
        bool IsPlaying { get; }

        /// <summary>
        /// The duration of the video in a `TimeSpan` object
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// Total number of frames in the video that has been loaded
        /// </summary>
        long TotalFrames { get; }

        /// <summary>
        /// Rate of frame playback in frames per second of the loaded video
        /// </summary>
        float FrameRate { get; }

        /// <summary>
        /// The currently showing frame in the video player
        /// </summary>
        long CurrentFrame { get; }

        /// <summary>
        /// The normalized position of the current frame in the [0,1] rannge
        /// </summary>
        float CurrentPosition { get; }

        /// <summary>
        /// The time gap between the video start and the current frame showing
        /// </summary>
        TimeSpan CurrentTimeSpan { get; }

        /// <summary>
        /// Opens a video for loading via the path. `autoPlay` flag decides if the video playback should start immediately after successful loading.
        /// </summary>
        void Open(string path, bool autoPlay);

        /// <summary>
        /// Starts playing/resumes the video
        /// </summary>
        void Play();

        /// <summary>
        /// Pauses the video playback
        /// </summary>
        void Pause();

        /// <summary>
        /// Stop the video playback
        /// </summary>
        void Stop();

        /// <summary>
        /// Seeks the playback to the given frame from the start of the video
        /// </summary>
        void SeekFrame(long frame);

        /// <summary>
        /// Seeks the playback to a frame `timeSpan` duration from the start of the video
        /// </summary>
        void SeekTimeSpan(TimeSpan timeSpan);

        /// <summary>
        /// Seeks the playabck to a frame `position` percentage from the start of the video. Eg. .5f seeks it to the middle of the video
        /// </summary>
        void SeekPosition(float position);

        /// <summary>
        /// Seeks to a frame `frameDelta` frames away from the current frame
        /// </summary>
        void JumpFrames(long frameDelta);

        /// <summary>
        /// Seeks to a frame `timeSpanDelta` duration away from the current frame
        /// </summary>
        void JumpTimeSpan(TimeSpan timeSpanDelta);

        /// <summary>
        /// Seeks to a frame `positionDelta` percentage from the current frame. Eg. -25f when `CurrentPosition` is .75f will take it to the middle of the video
        /// </summary>
        void JumpPosition(float positionDetla);
    }
}
