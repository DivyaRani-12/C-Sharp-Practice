using System;
using System.Collections.Generic;

namespace GenericFactoryDemo
{
    public interface IFactory<T> where T : new()
    {
        T Create();
        void Release(T instance);
    }

    public class SimpleFactory<T> : IFactory<T> where T : new()
    {
        public T Create()
        {
            Console.WriteLine("SimpleFactory: Creating new instance");
            return new T();
        }

        public void Release(T instance)
        {
            Console.WriteLine("SimpleFactory: Release called (ignored)");
        }
    }
    public class PooledFactory<T> : IFactory<T> where T : new()
    {
        private readonly Stack<T> pool = new Stack<T>();
        private readonly int maxPoolSize;

        public int PoolSize => pool.Count;

        public PooledFactory(int maxPoolSize = 10)
        {
            this.maxPoolSize = maxPoolSize;
        }

        public T Create()
        {
            if (pool.Count > 0)
            {
                Console.WriteLine("PooledFactory: Reusing object from pool");
                return pool.Pop();
            }

            Console.WriteLine("PooledFactory: Creating new instance");
            return new T();
        }

        public void Release(T instance)
        {
            if (pool.Count < maxPoolSize)
            {
                pool.Push(instance);
                Console.WriteLine($"PooledFactory: Returned to pool (Pool Size = {pool.Count})");
            }
            else
            {
                Console.WriteLine("PooledFactory: Pool full, object discarded");
            }
        }
    }
    public class CachedFactory<TKey, TValue> where TValue : new()
    {
        private readonly Dictionary<TKey, TValue> cache = new Dictionary<TKey, TValue>();

        public TValue GetOrCreate(TKey key)
        {
            if (!cache.ContainsKey(key))
            {
                Console.WriteLine($"Creating new value for key: {key}");
                cache[key] = new TValue();
            }
            else
            {
                Console.WriteLine($"Returning cached value for key: {key}");
            }

            return cache[key];
        }

        public void Clear()
        {
            cache.Clear();
            Console.WriteLine("Cache cleared");
        }
    }
    public class Connection
    {
        public int ConnectionId { get; set; }
        public bool IsOpen { get; set; }

        public Connection()
        {
            ConnectionId = Random.Shared.Next(1000);
            IsOpen = true;
            Console.WriteLine($"Connection Created: {ConnectionId}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SIMPLE FACTORY ===");

            IFactory<Connection> simpleFactory = new SimpleFactory<Connection>();

            Connection s1 = simpleFactory.Create();
            Connection s2 = simpleFactory.Create();

            simpleFactory.Release(s1);

            Console.WriteLine();

            Console.WriteLine("=== POOLED FACTORY ===");

            IFactory<Connection> pooledFactory = new PooledFactory<Connection>(5);

            Connection conn1 = pooledFactory.Create();
            Connection conn2 = pooledFactory.Create();

            pooledFactory.Release(conn1);

            Connection conn3 = pooledFactory.Create(); // Should reuse conn1

            pooledFactory.Release(conn2);
            pooledFactory.Release(conn3);

            Console.WriteLine();

            Console.WriteLine("=== CACHED FACTORY ===");

            CachedFactory<string, Connection> cachedFactory = new CachedFactory<string, Connection>();

            Connection c1 = cachedFactory.GetOrCreate("DB1");
            Connection c2 = cachedFactory.GetOrCreate("DB2");

            Connection c3 = cachedFactory.GetOrCreate("DB1");

            cachedFactory.Clear();

            Console.ReadLine();
        }
    }
}