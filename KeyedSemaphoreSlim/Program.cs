using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeyedSemaphoreSlim
{
    class Program
    {
        public static void DoWork(int pKey)
        {
            KeyedSemaphoreSlim.Enter(pKey.ToString());


            Console.WriteLine("Task {0} starting. Key; {1}", Thread.CurrentThread.ManagedThreadId, pKey);
            Thread.Sleep(2000);
            Console.WriteLine("Task {0} finish. Key; {1}", Thread.CurrentThread.ManagedThreadId, pKey);

            KeyedSemaphoreSlim.Leave(pKey.ToString());
        }
        static void Main(string[] args)
        {
            Task[] Tasks = new Task[10];
            for (int i = 0; i < 10; i++)
            {

                var mKey = (i % 2) == 0 ? 1 : 2;

                Tasks[i] = Task.Run(() => DoWork(mKey));
            }
            Task.WaitAll(Tasks);
            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();
        }
    }
}
