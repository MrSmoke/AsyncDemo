using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class1
    {

        public void DoWork()
        {
            var firstTask = Task.Factory.StartNew(() =>
            {
                var rnd = new Random();
                var dates = new DateTime[100];
                var buffer = new byte[8];
                var ctr = dates.GetLowerBound(0);

                while (ctr <= dates.GetUpperBound(0))
                {
                    rnd.NextBytes(buffer);

                    var ticks = BitConverter.ToInt64(buffer, 0);
                    if (ticks <= DateTime.MinValue.Ticks | ticks >= DateTime.MaxValue.Ticks)
                        continue;

                    dates[ctr] = new DateTime(ticks);
                    ctr++;
                }

                //DateTime[]
                return dates;
            });

            var continuationTask = firstTask.ContinueWith(antecedent =>
            {
                DateTime[] dates = antecedent.Result;
                var earliest = dates[0];
                var latest = earliest;

                for (var ctr = dates.GetLowerBound(0) + 1; ctr <= dates.GetUpperBound(0); ctr++)
                {
                    if (dates[ctr] < earliest) earliest = dates[ctr];
                    if (dates[ctr] > latest) latest = dates[ctr];
                }

                Console.WriteLine("Earliest date: {0}", earliest);
                Console.WriteLine("Latest date: {0}", latest);
            });

            continuationTask.Wait();
        }
    }
}
