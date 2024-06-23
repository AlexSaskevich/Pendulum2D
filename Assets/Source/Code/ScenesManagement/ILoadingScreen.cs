using Source.Code.Infrastructure;

namespace Source.Code.ScenesManagement
{
    public interface ILoadingScreen : IScreen
    {
        public ProgressBar ProgressBar { get; }
    }
}