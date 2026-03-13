using System;
using System.Collections.Generic;

namespace Exercise4
{
    public interface IPipelineStep<TIn, TOut>
    {
        TOut Process(TIn input);
    }

    public class Pipeline<T>
    {
        private Func<T, T> pipeline = input => input;

        public Pipeline<T> AddStep(Func<T, T> step)
        {
            Func<T, T> CurrentPipeline = pipeline;
            pipeline = input => step(CurrentPipeline(input));
            return this;
        }

        public T Execute(T input)
        {
            return pipeline(input);
        }
    }

    // Missing class
    public class TransformPipeline
    {
        public static PipelineBuilder<TInput> Create<TInput>(TInput input)
        {
            return new PipelineBuilder<TInput>(input);
        }
    }

    public class PipelineBuilder<T>
    {
        private T current;

        public PipelineBuilder(T initial)
        {
            current = initial;
        }

        public PipelineBuilder<TNext> Then<TNext>(Func<T, TNext> transform)
        {
            TNext next = transform(current);
            return new PipelineBuilder<TNext>(next);
        }

        public T GetResult() => current;
    }

    class Program
    {
        static void Main()
        {
            string csvData = "Divya,30,divya@gmail.com";

            var result = TransformPipeline.Create(csvData)
                .Then(csv => csv.Split(','))
                .Then(parts => new
                {
                    Name = parts[0],
                    Age = int.Parse(parts[1]),
                    Email = parts[2]
                })
                .Then(user => $"User: {user.Name}, Age: {user.Age}")
                .GetResult();

            Console.WriteLine(result);
        }
    }
}