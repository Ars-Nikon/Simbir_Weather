using System.Threading.Tasks;

namespace TimerWorker
{
    public interface ITimerWorker
    {
        Task StartTimerAsync();
    }
}
