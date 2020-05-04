using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Adrenak.MediaPlayer;

public class Demo : MonoBehaviour {
    IMediaPlayer player;

    void Start() {
        player = FindObjectOfType<UnityMediaPlayer>();
        player.Open("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", true);
    }

    private void Update() {
        Debug.Log(
            "Duration " + player.Duration + " has " + player.Frames + "frames@" + player.FrameRate + "\n" +
            "Currently at frame " + player.CurrentFrame + " with position " + player.CurrentPosition + " and " + player.CurrentTimeSpan.TotalSeconds + "seconds have elapsed"
        );
    }
}
