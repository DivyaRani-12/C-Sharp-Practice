using System;
using System.Collections.Generic;


namespace Exercise1
{
    public class CircularBuffer<T>
    {
        private T[] buffer;
        private int head;
        private int tail;
        private int count;

        public int Capacity {get;}
        public int Count=>count;
        public bool IsFull=>count==Capacity;
        public bool IsEmpty=>count==0;

        public CircularBuffer(int capacity)
        {
            if(capacity<=0)
                throw new ArgumentException("capacity must be postive");

            Capacity=capacity;
            buffer=new T[capacity];
            head=0;
            tail=0;
            count=0;
        }

        public void Add(T item)
        {
            buffer[tail]=item;
            tail=(tail+1)%Capacity;

            if (IsFull)
            {
                head=(head+1)%Capacity;
            }
            else
            {
                count++;
            }
        }

        public T GetOldest()
        {
            if(IsEmpty)
                throw new InvalidOperationException("Buffer is Empty"); 

            return buffer[head];
        }

        public T Remove()
        {
            if(IsEmpty)
                throw new InvalidOperationException("Buffer is empty");

            T item=buffer[head];
            head=(head+1)%Capacity;
            count--;

            return item;
        }

        public void Clear()
        {
            head=0;
            tail=0;
            count=0;
        }

        public IEnumerable<T> GetAll()
        {
            for(int i = 0; i < count; i++)
            {
                yield return buffer[(head+i)%Capacity];
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CircularBuffer<int> buffer=new CircularBuffer<int>(3);

            buffer.Add(1);
            buffer.Add(2);
            buffer.Add(3);
            buffer.Add(4);

            System.Console.WriteLine("Buffer contents:");

            foreach(int item in buffer.GetAll())
            {
                System.Console.WriteLine(item);
            }

            System.Console.WriteLine("Oldest item:"+buffer.GetOldest());

            System.Console.WriteLine("Removed:"+buffer.Remove());

            System.Console.WriteLine("Buffer after removal");

            foreach(int item in buffer.GetAll())
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
