# MediaPlayer
A standardised media player interface for Unity. Aimed largely towards being able to make video player UI without worrying about the underlying video player.

# The `IMediaPlayer` interface
## Properties
* `MediaTexture` The `Texture` object that represents the texture on which the video plays
* `IsReady` If the video player has loaded the video and the video can be played or not
* `IsPlaying` If the video is presently playing. False when the video is not loaded or paused.
* `Duration` The duration of the video in a `TimeSpan` object
* `TotalFrames` Total number of frames in the video that has been loaded
* `FrameRate` Rate of frame playback in frames per second of the loaded video
* `CurrentFrame` The currently showing frame in the video player
* `CurrentPosition` The normalized position of the current frame in the [0,1] rannge
* `CurrentTimeSpan` The time gap between the video start and the current frame showing

## Methods
* `Open(string path, bool autoPlay)` Opens a video for loading via the path. `autoPlay` flag decides if the video playback should start immediately after successful loading.
* `Play` Starts playing/resumes the video
* `Pause` Pauses the video playback
* `Stop` Stop the video playback
  
* `SeekFrame(long frame)` Seeks the playback to the given frame from the start of the video
* `SeekTimeSpan(TimeSpan timeSpan)` Seeks the playback to a frame `timeSpan` duration from the start of the video
* `SeekPosition(float position)` Seeks the playabck to a frame `position` percentage from the start of the video. Eg. .5f seeks it to the middle of the video
  
* `JumpFrames(long frameDelta)` Seeks to a frame `frameDelta` frames away from the current frame
* `JumpTimeSpan(TimeSpan timeSpanDelta)` Seeks to a frame `timeSpanDelta` duration away from the current frame
* `JumpPosition(float positionDelta)` Seeks to a frame `positionDelta` percentage from the current frame. Eg. -25f when `CurrentPosition` is .75f will take it to the middle of the video

## Events
* `OnReady` Fired when a video is loaded and it capable of being player
* `OnError` Fired when there is an error in loading the video
* `OnPlay` Fired when the video starts playing for the first time or is resumed in the middle of the playback
* `OnPause` Fired when the video is paused
* `OnStop` Fired when the playback is stopped
* `OnSeek` Fired when the media player seeks to another frame
* `OnJump` Fired when the media played jumps forward or backward by any number of frames

# Notes
The project contains a `IMediaPlayer` implementation for Unity's VideoPlayer component. Please see `SampleMediaPlayer` under `Samples/` 

# Contact
Vatsal "adrenak" Ambastha  
[@www](http://www.vatsalambastha.com)  
[@github](https://www.github.com/adrenak)  
[@npm](https://www.npmjs.com/~adrenak)