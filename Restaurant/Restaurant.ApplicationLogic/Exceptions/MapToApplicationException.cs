using Restaurant.ApplicationLogic.Interfaces;
using Restaurant.Domain.Exceptions;
using System;

namespace Restaurant.ApplicationLogic.Exceptions
{
    internal class MapToApplicationException : IMapToApplicationException
    {
        public Exception Map(Exception exception)
        {
            if (exception.GetType() == typeof(RestaurantException))
            {
                var restaurantException = (RestaurantException)exception;
                return new RestaurantServerException(restaurantException.Message,
                    restaurantException.ClassObjectThrown,
                    restaurantException.Context, exception);
            }

            return exception;
        }
    }
}
