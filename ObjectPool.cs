using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7._4
{

    public class ObjectPool<T>
    {
        private Func<T> objectInitializer;
        private Action<T> objectReset;
        private Queue<T> objectPool;

        public ObjectPool(Func<T> initializer, Action<T> reset)
        {
            objectInitializer = initializer;
            objectReset = reset;
            objectPool = new Queue<T>();
        }

        public T GetObject()
        {
            if (objectPool.Count > 0)
            {
                T obj = objectPool.Dequeue();
                return obj;
            }
            else
            {
                T newObj = objectInitializer();
                return newObj;
            }
        }

        public void ReturnObject(T obj)
        {
            objectReset(obj);
            objectPool.Enqueue(obj);
        }
    }
}
