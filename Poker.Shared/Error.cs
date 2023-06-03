using System;

namespace Poker.Shared
{
    public class Error
    {
        public Exception? Exception { get; init; }

        public string? Message { get; init; }

        public static Error WithMessage(string message) => new()
        {
            Message = message
        };

        public static Error WithException(Exception exception) => new()
        {
            Exception = exception
        };

        public static Error Empty => new();
    }
}