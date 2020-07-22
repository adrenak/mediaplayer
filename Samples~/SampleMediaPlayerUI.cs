using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Adrenak.MediaPlayer {
    public class SampleMediaPlayerUI : MonoBehaviour {
        [SerializeField] InputField urlInput;
        [SerializeField] Toggle autoPlay;
        [SerializeField] RawImage videoSurface;
        [SerializeField] Text status;
        [SerializeField] Image seekBar;

        IVideoPlayer player;

        public void Load() {
            player = FindObjectOfType<UnityVideoPlayer>();
            player.Open(urlInput.text, autoPlay.isOn);

            player.OnReady += () => {
                var texture = player.MediaTexture;
                videoSurface.texture = texture;

                var fitter = videoSurface.GetComponent<AspectRatioFitter>();
                fitter.aspectRatio = (float)texture.width / texture.height;

                StartCoroutine(ShowStatus("Ready", 1));
            };
            player.OnError += error => StartCoroutine(ShowStatus(error.Message, 1));
            player.OnPlay += () => StartCoroutine(ShowStatus("Playing", 1));
            player.OnPause += () => StartCoroutine(ShowStatus("Paused", 1));
        }

        IEnumerator ShowStatus(object msg, float delay = 0) {
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

        public void Stop() {
            player.Stop();
        }

        public void Forward() {
            player.JumpTimeSpan(TimeSpan.FromSeconds(5));
            StartCoroutine(ShowStatus("Jumping forward", 1));
        }

        public void Backward() {
            player.JumpTimeSpan(TimeSpan.FromSeconds(-5));
            StartCoroutine(ShowStatus("Jumping backward", 1));
        }
    }
}
