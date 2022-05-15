using System;

namespace Restaurant.ApplicationLogic.Interfaces
{
    public interface IMapToApplicationException
    {
        Exception Map(Exception exception);
    }
}
