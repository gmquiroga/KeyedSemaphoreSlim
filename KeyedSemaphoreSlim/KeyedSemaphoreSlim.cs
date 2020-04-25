using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeyedSemaphoreSlim
{
    public class KeyedSemaphoreSlim
    {
        private static ConcurrentDictionary<string, SemaphoreSlim> cConcurrentEntities = new ConcurrentDictionary<string, SemaphoreSlim>();
        
        public static void Enter(string pKey)
        {
            SemaphoreSlim mSemaphore;

            if (!cConcurrentEntities.TryGetValue(pKey, out mSemaphore))
            {
                mSemaphore = new SemaphoreSlim(1);

                // agregamos el semaforo para la clave
                cConcurrentEntities.TryAdd(pKey, mSemaphore);
            }

            mSemaphore.Wait();
            
        }

        public static void Leave(string pKey)
        {
            SemaphoreSlim mSemaphore;

            cConcurrentEntities.TryGetValue(pKey, out mSemaphore);

            mSemaphore.Release();
        }
    }
}
