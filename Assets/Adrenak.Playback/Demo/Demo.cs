using System;
using System.Collections.Generic;
using UnityEngine;
using Adrenak.MediaPlayer;

public class Demo : MonoBehaviour {
    IMediaPlayer player;

    void Start() {
        player = FindObjectOfType<UnityMediaPlayer>();
        player.Open("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", true);
    }

    [ContextMenu("Jump forward some frames")]
    public void JumpFramesForward(){
        player.JumpFrames(1000);
    }

    [ContextMenu("Jump backward some frames")]
    public void JumpFramesBackward() {
        player.JumpFrames(-1000);
    }

    [ContextMenu("Jump forward some timespan")]
    public void JumpTimeForward() {
        player.JumpTimeSpan(TimeSpan.FromSeconds(10));
    }

    [ContextMenu("Jump backward some timespan")]
    public void JumpTimeBackward() {
        player.JumpTimeSpan(TimeSpan.FromSeconds(-10));
    }

    [ContextMenu("Jump forward some position")]
    public void JumpPositionForward() {
        player.JumpPosition(.1f);
    }

    [ContextMenu("Jump backward some position")]
    public void JumpPositionBackward() {
        player.JumpPosition(-.1f);
    }

    private void Update() {
        Debug.Log(
            "Duration " + player.Duration + " has " + player.TotalFrames + "frames@" + player.FrameRate + "\n" +
            "Currently at frame " + player.CurrentFrame + " with position " + player.CurrentPosition + " and " + player.CurrentTimeSpan.TotalSeconds + "seconds have elapsed"
        );
    }
}
