namespace Adrenak.MediaPlayer{
    public interface ISubtitlesParser {
        /// <summary>
        /// Get's the subtitle block in the file for the specified time
        /// </summary>
        SubtitleBlock GetByTime(double time);
    }
}
