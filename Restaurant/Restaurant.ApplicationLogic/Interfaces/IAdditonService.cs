using Restaurant.ApplicationLogic.DTO;
using System;
using System.Collections.Generic;

namespace Restaurant.ApplicationLogic.Interfaces
{
    public interface IAdditonService : IService
    {
        AdditionDto Get(Guid id);
        IEnumerable<AdditionDto> GetAll();
    }
}
