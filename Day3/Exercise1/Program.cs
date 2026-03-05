using System;
using System.Collections.Generic;

namespace Exercise1
{
    class ArrayUtils
    {
        public static int FindMax(int[] array)
        {
            if(array==null || array.Length == 0 )
                throw new ArgumentException("Array can not be null or empty");
            
            int max=array[0];
            foreach(int num in array)
            {
                if (num > max)
                {
                    max=num;
                }
                
            }
            return max;
        }
        public static int FindMin(int[] array)
        {
            if(array==null || array.Length == 0 )
                throw new ArgumentException("Array can not be null or empty");
            

            int min=array[0];
            foreach(int num in array)
            {
                if (num < min)
                {
                    min=num;
                }
                
            }
            return min;
        }
        public static double CalculateAvg(int[] array)
        {
            if(array==null || array.Length == 0 )
                throw new ArgumentException("Array can not be null or empty");
            
            int sum=0;
            foreach(int num in array)
            {
                sum+=num;
            }
            return (double)sum/array.Length;

        }

        public static int[] RemoveDuplicates(int[] array)
        {
            if(array==null || array.Length == 0 )
                throw new ArgumentException("Array can not be null or empty");
            
            HashSet<int> set = new HashSet<int>(array);
            int[] result = new int[set.Count];
            set.CopyTo(result);
            return result;

            
        } 

        public static void Reverse(int[] array)
        {
            int left = 0,right=array.Length-1;
            while (left < right)
            {
                int temp=array[left];
                array[left]=array[right];
                array[right]=temp;
                left++;
                right--;
            }
        }

        public static void Rotate(int[] array, int positions)
        {
            int n = array.Length;
            positions = positions%n;

            Reverse(array);
            ReversePart(array,0,positions-1);
            ReversePart(array,positions,n-1);
        }

        public static void ReversePart(int[] array,int start,int end)
        {
            while (start < end)
            {
                int temp=array[start];
                array[start]=array[end];
                array[end]=temp;
                start++;
                end--;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers={5,3,6,2,9,3,4,5,6};

            System.Console.WriteLine($"Array:{string.Join(", ",numbers)}");
            System.Console.WriteLine($"Max:{ArrayUtils.FindMax(numbers)}");
            System.Console.WriteLine($"Min:{ArrayUtils.FindMin(numbers)}");
            System.Console.WriteLine($"CalculateAvg:{ArrayUtils.CalculateAvg(numbers):F2}");

            int[] unique = ArrayUtils.RemoveDuplicates(numbers);
            System.Console.WriteLine($"Unique:{string.Join(", ",unique)}");

            ArrayUtils.Reverse(numbers);
            System.Console.WriteLine($"Reversed: {string.Join(", ",numbers)}");

            ArrayUtils.Rotate(numbers,2);
            System.Console.WriteLine($"Rotated by 2: {string.Join(", ",numbers)}");


        }
    }
}