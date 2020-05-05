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
                var texture = player.Texture;
                videoSurface.texture = texture;
                
                var fitter = videoSurface.GetComponent<AspectRatioFitter>();
                fitter.aspectRatio = (float)texture.width / texture.height;

                StartCoroutine(ShowStatus("Ready", 1));
            };
            player.OnPlay += () => StartCoroutine(ShowStatus("Playing", 1));
            player.OnPause += () => StartCoroutine(ShowStatus("Paused", 1));
        }

        IEnumerator ShowStatus(object msg, float delay = 0){
            status.text = msg.ToString();
            yield return new WaitForSeconds(delay);
            status.text = string.Empty;
        }

        private void Update() {
            if (player == null) return;
            seekBar.fillAmount = player.CurrentPosition;
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
            StartCoroutine(ShowStatus("Jumping forward"));
        }

        public void Backward() {
            player.JumpTimeSpan(TimeSpan.FromSeconds(-5));
            StartCoroutine(ShowStatus("Jumping backward"));
        }
    }
}
