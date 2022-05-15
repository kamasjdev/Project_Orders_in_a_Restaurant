using System;

namespace Restaurant.Domain.Exceptions
{
    public class RestaurantServerException : Exception
    {
        public string ClassObjectThrown { get; }
        public string Context { get; }

        public RestaurantServerException(string message, string classObjectThrown, string context) : base(message)
        {
            ClassObjectThrown = classObjectThrown;
            Context = context;
        }

        public RestaurantServerException(string message, string classObjectThrown, string context, Exception innerException) : base(message, innerException)
        {
            ClassObjectThrown = classObjectThrown;
            Context = context;
        }
    }
}
