using System;

namespace Restaurant.UI.Exceptions
{
    internal class RestaurantClientException : Exception
    {
        public string ClassObjectThrown { get; }
        public string Context { get; }

        public RestaurantClientException(string message, string classObjectThrown, string context) : base(message)
        {
            ClassObjectThrown = classObjectThrown;
            Context = context;
        }

        public RestaurantClientException(string message, string classObjectThrown, string context, Exception innerException) : base(message, innerException)
        {
            ClassObjectThrown = classObjectThrown;
            Context = context;
        }
    }
}
