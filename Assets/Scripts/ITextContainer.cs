public interface ITextContainer
{
    public string Text { get; set; }
    public void Highlight(bool state);
}