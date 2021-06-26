using System;
using System.Threading.Tasks;

namespace TimerWorker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string configureFilePath = "appsettings.json";
            ITimerWorker timerWorker = new TimerWorker(configureFilePath);

            await timerWorker.StartTimerAsync();
        }
    }
}
