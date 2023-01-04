using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Gateways
{
    public interface IDataTypeRepository
    {
        List<DataType> GetAll();
    }
}
