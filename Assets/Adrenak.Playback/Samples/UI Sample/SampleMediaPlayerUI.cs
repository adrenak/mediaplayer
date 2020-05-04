using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Adrenak.MediaPlayer {
    public class SampleMediaPlayerUI : MonoBehaviour {
        [SerializeField] RawImage videoSurface;
        [SerializeField] Text status;
        [SerializeField] Image seekBar;

        IMediaPlayer player;

        void Start() {
            player = FindObjectOfType<UnityMediaPlayer>();
            player.Open("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", false);

            player.OnReady += () => {
                videoSurface.texture = player.Texture;
                StartCoroutine(ShowStatus("Ready", 1));
            };
            player.OnPlay += () => StartCoroutine(ShowStatus("Playing", 1));
            player.OnPause += () => StartCoroutine(ShowStatus("Paused", 1));
            player.OnJump += x => StartCoroutine(ShowStatus($"Jumping " + (x > 0 ? "forward" : "backward"), 1));
        }

        IEnumerator ShowStatus(object msg, float delay = 0){
            status.text = msg.ToString();
            yield return new WaitForSeconds(delay);
            status.text = string.Empty;
        }

        private void Update() {
            if (player == null) return;
            seekBar.fillAmount = player.CurrentPosition;
            Debug.Log(player.IsPlaying);
        }

        public void Toggle() {
            if (!player.IsReady) return;

            if (player.IsPlaying)
                player.Pause();
            else
                player.Play();
        }

        public void Forward(){
            player.JumpTimeSpan(TimeSpan.FromSeconds(5));
        }

        public void Backward() {
            player.JumpTimeSpan(TimeSpan.FromSeconds(-5));
        }
    }
}
