using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TimerWorker
{
    class TimerWorker : ITimerWorker
    {
        private readonly DbContextOptions<EventContext> _dbOptions;
        private readonly int _interval;
        private readonly int _maxDegreeOfParallelism;
        private readonly string _notificationServerHost;
        private const string CONSOLE_SEPARATOR = "-------------------------";

        public TimerWorker(string configureFilePath)
        {
            dynamic json = JsonConvert.DeserializeObject(File.ReadAllText(configureFilePath));

            string connectionString = json.ConnectionStrings.DefaultConnection;
            _dbOptions = new DbContextOptionsBuilder<EventContext>().UseSqlServer(connectionString).Options;
            _interval = json.TimerConfiguration.Interval;
            _maxDegreeOfParallelism = json.TimerConfiguration.MaxDegreeOfParallelism;
            _notificationServerHost = json.NotificationServerHost;
        }

        public Task StartTimerAsync()
        {
            while (true)
            {
                try
                {
                    Task.Delay(_interval).Wait();

                    using (EventContext db = new EventContext(_dbOptions))
                    {
                        int countSent = db.Events.Where(e => e.Done).Count();
                        int countQueue = db.Events.Count() - countSent;

                        Console.WriteLine($"Sent: {countSent}\nQueue: {countQueue}");

                        foreach (var t in db.Events)
                        {
                            if (DateTime.Now > t.DateSendMessage && !t.Done)
                            {
                                var result = SendReadyNotificationIdToServer(t.Id);

                                if (result == HttpStatusCode.OK)
                                {
                                    t.Done = true;
                                    Console.WriteLine($"Send {t.Id}");
                                }
                            }
                        }

                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    string exeptionInfo = string.Format(
                        "{0}\n{1}\n{2}\n{3}\n{4}\n{5}",
                        CONSOLE_SEPARATOR, ex.Message, ex.Data, ex.Source, ex.StackTrace, CONSOLE_SEPARATOR);
                    Console.WriteLine(exeptionInfo);
                }
            }
        }

        private HttpStatusCode SendReadyNotificationIdToServer(int notificationId)
        {
            HttpWebRequest rq = (HttpWebRequest)WebRequest.Create(_notificationServerHost);
            rq.Method = WebRequestMethods.Http.Post;

            string data = $"notificationId={notificationId}";
            byte[] postData = Encoding.UTF8.GetBytes(data);
            rq.ContentType = "application/x-www-form-urlencoded";
            rq.ContentLength = postData.Length;
            using (var dataStream = rq.GetRequestStream())
            {
                dataStream.Write(postData, 0, postData.Length);
            }

            HttpWebResponse resp = (HttpWebResponse)rq.GetResponse();

            return resp.StatusCode;
        }
    }
}
