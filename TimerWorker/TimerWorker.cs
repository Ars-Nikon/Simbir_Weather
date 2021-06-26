﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TimerWorker
{
    class TimerWorker : ITimerWorker
    {
        //private readonly DbContextOptions<> _db;
        private readonly int _interval;
        private readonly int _maxDegreeOfParallelism;
        private readonly string _notificationServerHost;

        public TimerWorker(string configureFilePath)
        {
            dynamic json = JsonConvert.DeserializeObject(File.ReadAllText(configureFilePath));

            string connectionString = json.ConnectionStrings.DefaultConnection;
            //_dbOptions = new DbContextOptionsBuilder<>().UseNpgsql(connectionString).Options;
            _interval = json.TimerConfiguration.Interval;
            _maxDegreeOfParallelism = json.TimerConfiguration.MaxDegreeOfParallelism;
            _notificationServerHost = json.NotificationServerHost;
        }

        public async Task StartTimerAsync()
        {
            var result = SendReadyNotificationIdToServer(111);
            while (true)
            {
                try
                {
                    await Task.Delay(_interval);

                    ParallelOptions parallelOptions = new ParallelOptions()
                    {
                        MaxDegreeOfParallelism = _maxDegreeOfParallelism
                    };

                    //Parallel.ForEach(_dateTimes, parallelOptions, t =>
                    //{
                    //    if (DateTime.Now > t.DateTime && !t.IsDone)
                    //    {
                    //        var result = SendReadyNotificationIdToServer(t.id);

                    //        if (result == HttpStatusCode.OK)
                    //        {
                    //            t.IsDone = true;
                    //        }
                    //    }
                    //});
                }
                catch
                {

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
