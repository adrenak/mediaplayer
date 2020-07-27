using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Adrenak.MediaPlayer {
    public class SubtitleDisplayer : MonoBehaviour {
        public float Time { get; set; }
        public float Offset { get; set; }
        public ISubtitlesParser Parser { get; set; }

        [SerializeField] bool isDisplaying;
        public bool IsDisplaying {
            get => isDisplaying;
            set {
                isDisplaying = value;
                text.enabled = value;
            }
        }
        [SerializeField] Text text;

        public IEnumerator Start() {
            text.text = string.Empty;
            SubtitleBlock currentSubtitle = null;

            while (true) {
                if (Parser == null) {
                    yield return null;
                    continue;
                }

                var subtitle = Parser.GetByTime(Time + Offset);
                if (subtitle != null) {
                    if (!subtitle.Equals(currentSubtitle)) {
                        currentSubtitle = subtitle;
                        text.text = currentSubtitle.Text;
                    }
                }
                else
                    text.text = string.Empty;

                yield return null;
            }
        }
    }
}
