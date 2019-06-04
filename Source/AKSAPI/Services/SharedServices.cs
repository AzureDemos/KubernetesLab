using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AKSAPI.Services
{
    public static class SharedServices
    {

        public static void SpikeCPU(object token)
        {
            int cpuUsage = 40;
            Parallel.For(0, 1, new Action<int>((int i) =>
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                while (!((CancellationToken)token).IsCancellationRequested)
                {
                    if (watch.ElapsedMilliseconds > cpuUsage)
                    {
                        Thread.Sleep(100 - cpuUsage);
                        watch.Reset();
                        watch.Start();
                    }
                }
            }));

        }

    }

}
