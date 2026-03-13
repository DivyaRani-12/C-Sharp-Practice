using System;

namespace Exercise5
{
    public class Result<T>
    {
        public T? Value { get; }
        public string Error { get; }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        private Result(T value)
        {
            Value = value;
            Error = string.Empty;
            IsSuccess = true;
        }

        private Result(string error)
        {
            Value = default;
            Error = error;
            IsSuccess = false;
        }

        public static Result<T> Success(T value) => new(value);
        public static Result<T> Failure(string error) => new(error);

        public Result<TOutput> Map<TOutput>(Func<T, TOutput> transform)
        {
            return IsSuccess
                ? Result<TOutput>.Success(transform(Value!))
                : Result<TOutput>.Failure(Error);
        }

        public Result<TOutput> Bind<TOutput>(Func<T, Result<TOutput>> transform)
        {
            return IsSuccess
                ? transform(Value!)
                : Result<TOutput>.Failure(Error);
        }

        public TOutput Match<TOutput>(
            Func<T, TOutput> onSuccess,
            Func<string, TOutput> onFailure)
        {
            return IsSuccess
                ? onSuccess(Value!)
                : onFailure(Error);
        }
    }

    public class Program
    {
        static Result<string> ParseInt(string input)
        {
            if (int.TryParse(input, out int value))
                return Result<string>.Success(input);

            return Result<string>.Failure("Invalid number");
        }

        static Result<int> ConvertToInt(string input)
        {
            if (int.TryParse(input, out int value))
                return Result<int>.Success(value);

            return Result<int>.Failure("Conversion failed");
        }

        static Result<int> MultiplyByTwo(int number)
        {
            return Result<int>.Success(number * 2);
        }

        static void Main()
        {
            var result = ParseInt("42")
                .Bind(ConvertToInt)
                .Bind(MultiplyByTwo);

            string output = result.Match(
                onSuccess: value => $"Success: {value}",
                onFailure: error => $"Error: {error}"
            );

            Console.WriteLine(output);
        }
    }
}