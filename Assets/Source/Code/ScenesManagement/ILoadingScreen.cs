namespace Source.Code.ScenesManagement
{
    public interface ILoadingScreen
    {
        public ProgressBar ProgressBar { get; }

        public void Show();
        public void Hide();
    }
}