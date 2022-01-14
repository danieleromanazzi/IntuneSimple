using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntuneSimple
{
    public class Scheduler
    {
        private static readonly CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

        public static void Run(Action action, TimeSpan period, CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!CancellationTokenSource.IsCancellationRequested)
                {
                    await Task.Delay(period, cancellationToken);

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        action();
                    }
                }
            });
        }

        public static void Stop()
        {
            CancellationTokenSource.Cancel();
        }

        public static void Run(Action action, TimeSpan period)
        {
            Run(action, period, CancellationToken.None);
        }
    }
}
