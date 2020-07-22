using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Adrenak.MediaPlayer {
    public class SRTParser : ISubtitlesParser {
        enum ReadState {
            Index,
            Time,
            Text
        }

        readonly List<SubtitleBlock> subtitles;

        public SRTParser(string subtitleContent) {
            subtitles = Load(subtitleContent);
        }

        List<SubtitleBlock> Load(string content) {
            if (string.IsNullOrEmpty(content)) {
                Debug.LogError("Subtitle contents are null or empty");
                return null;
            }

            var lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var currentState = ReadState.Index;
            var subs = new List<SubtitleBlock>();
            int currentIndex = 0;
            double currentFrom = 0, currentTo = 0;
            var currentText = string.Empty;

            for (var l = 0; l < lines.Length; l++) {
                var line = lines[l];

                switch (currentState) {
                    case ReadState.Index:
                        if (int.TryParse(line, out int index)) {
                            currentIndex = index;
                            currentState = ReadState.Time;
                        }
                        break;

                    case ReadState.Time:
                        line = line.Replace(',', '.');
                        var parts = line.Split(new[] { "-->" }, StringSplitOptions.RemoveEmptyEntries);

                        // Parse the timestamps
                        if (parts.Length == 2) {
                            if (TimeSpan.TryParse(parts[0], out TimeSpan fromTime)) {
                                TimeSpan toTime;
                                if (TimeSpan.TryParse(parts[1], out toTime)) {
                                    currentFrom = fromTime.TotalSeconds;
                                    currentTo = toTime.TotalSeconds;
                                    currentState = ReadState.Text;
                                }
                            }
                        }
                        break;

                    case ReadState.Text:
                        if (currentText != string.Empty)
                            currentText += "\r\n";

                        currentText += line;

                        // When we hit an empty line, consider it the end of the text
                        if (string.IsNullOrEmpty(line) || l == lines.Length - 1) {
                            // Create the SubtitleBlock with the data we've aquired 
                            subs.Add(new SubtitleBlock(currentIndex, currentFrom, currentTo, currentText));

                            // Reset stuff so we can start again for the next block
                            currentText = string.Empty;
                            currentState = ReadState.Index;
                        }
                        break;
                }
            }
            return subs;
        }

        int lastServedBlockIndex = 0;
        public SubtitleBlock GetByTime(double time) {
            if (time < 0) return null;
            if (time > subtitles.Last().To) return null;

            if (subtitles.Count == 0) return null;

            if (time > subtitles.Last().To) return null;
            if (time > subtitles[lastServedBlockIndex].From && time < subtitles[lastServedBlockIndex].To) return subtitles[lastServedBlockIndex];

            SubtitleBlock getBlock(int startIndex) {
                for (int i = startIndex; i < subtitles.Count; i++) {
                    var sub = subtitles[i];
                    if (sub.To > time && sub.From < time) {
                        lastServedBlockIndex = i;
                        return sub;
                    }
                }
                return null;
            }

            var result = getBlock(lastServedBlockIndex);
            if (result != null)
                return result;
            else {
                result = getBlock(0);
                if (result != null)
                    return result;
            }
            return null;
        }
    }
}
