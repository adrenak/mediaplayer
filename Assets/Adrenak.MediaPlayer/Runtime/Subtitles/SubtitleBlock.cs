namespace Adrenak.MediaPlayer {
    public class SubtitleBlock {
        public int Index { get; private set; }
        public double Length { get; private set; }
        public double From { get; private set; }
        public double To { get; private set; }
        public string Text { get; private set; }

        public SubtitleBlock(int index, double from, double to, string text) {
            Index = index;
            From = from;
            To = to;
            Length = to - from;
            Text = text;
        }
    }
}