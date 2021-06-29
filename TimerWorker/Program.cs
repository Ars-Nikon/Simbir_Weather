using System;
using System.Threading.Tasks;

namespace TimerWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            string configureFilePath = "appsettings.json";
            ITimerWorker timerWorker = new TimerWorker(configureFilePath);

            timerWorker.StartTimerAsync().Start();
        }
    }
}
