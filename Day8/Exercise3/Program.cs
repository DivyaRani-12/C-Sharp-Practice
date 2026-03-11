using System;
using System.Collections.Generic;

public static class GenericUtilities
{
    public static T FindMax<T>(List<T> list) where T : IComparable<T>
    {
        if(list==null || list.Count==0)
            throw new ArgumentException("List cannot be null or emtpty");

        T max=list[0];

        foreach(T item in list)
        {
            if(item.CompareTo(max) > 0)
                max = item;
        }
        return max;

    }

    public static T FindMin<T>(List<T> list)where T : IComparable<T>
    {
        if(list==null || list.Count == 0)
            throw new ArgumentException("List cannot be null or empty ");

        T min=list[0];

        foreach(T item in list)
        {
            if(item.CompareTo(min) < 0)
                min=item;   
        }
        return min;

    }

    public static void Swap<T>(ref T a,ref T b)
    {
        T temp=a;
        a=b;
        b=temp;
    }

    public static T Clone<T>(T obj) where T: ICloneable
    {
        return (T)obj.Clone();
    }

    public static bool AreEqual<T>(T a,T b) where T : IEquatable<T>
    {
        if(a==null && b==null) return true;
        if(a==null || b==null) return false;

        return a.Equals(b);
    }

    public static List<TOutput> ConvertAll<TInput,TOutput>(
        List<TInput> input,
        Func<TInput, TOutput> converter)
    {
        List<TOutput> result = new List<TOutput>();

        foreach(TInput item in input)
        {
            result.Add(converter(item));
        }
        return result;
    }

    public static List<T> Filter<T>(List<T> list, Predicate<T> predicate)
    {
        List<T> result=new List<T>();

        foreach(T item in list)
        {
            if(predicate(item))
                result.Add(item);
        }
        return result;
    }

    public static (List<T> matching,List<T> notMatching) Partition<T>(
        List<T> list,
        Predicate<T> predicate)
    {
        List<T> matching = new List<T>();
        List<T> notMatching= new List<T>();

        foreach(T item in list)
        {
            if(predicate(item))
                matching.Add(item);
            else
                notMatching.Add(item);
        }
        return (matching,notMatching);


    }

    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers=new() {5,2,8,1,9};

            int max=GenericUtilities.FindMax(numbers);
            int min=GenericUtilities.FindMin(numbers);

            System.Console.WriteLine($"Max: {max}");
            System.Console.WriteLine($"Min: {min}");

            int x=10,y=20;
            GenericUtilities.Swap(ref x,ref y);

            System.Console.WriteLine($"After Swap:x={x},y={y}");

            List<string> names = new() {"Divya","Aishu","DII"};

            List<int> lengths = GenericUtilities.ConvertAll(names,n=>n.Length);

            System.Console.WriteLine("Name lengths:");
            foreach(var len in lengths)
                System.Console.WriteLine(len);

            var(even,odd)=GenericUtilities.Partition(numbers,n=>n%2==0);

            System.Console.WriteLine("Even numbers:");
            foreach(var e in even)
                System.Console.WriteLine(e);

            System.Console.WriteLine("Odd numbers:");
            foreach(var o in odd)
                System.Console.WriteLine(o);
        }
    }

}

