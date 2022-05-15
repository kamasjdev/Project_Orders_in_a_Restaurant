using System;

namespace Restaurant.Domain.Exceptions
{
    public class RestaurantException : Exception
    {
        public string ClassObjectThrown { get; }
        public string Context { get; }

        public RestaurantException(string message, string classObjectThrown, string context) : base(message)
        {
            ClassObjectThrown = classObjectThrown;
            Context = context;
        }

        public RestaurantException(string message, string classObjectThrown, string context, Exception innerException) : base(message, innerException)
        {
            ClassObjectThrown = classObjectThrown;
            Context = context;
        }
    }
}
