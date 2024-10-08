
namespace MiviaMaui.Interfaces
{
    public interface IDirectoryWatcherService : IDisposable
    {
        void StartWatching();
        void StopWatching();
    }
}
