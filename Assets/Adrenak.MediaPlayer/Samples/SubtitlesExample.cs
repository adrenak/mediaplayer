using System.IO;
using UnityEngine;

namespace Adrenak.MediaPlayer {
    public class SubtitlesExample : MonoBehaviour {
        public string path;
        public SubtitleDisplayer displayer;

        void Start() {
            var content = File.ReadAllText(path);
            displayer.Parser = new SRTParser(content);
        }

        void Update() {
            displayer.Time = Time.time + 200;
        }
    }
}
