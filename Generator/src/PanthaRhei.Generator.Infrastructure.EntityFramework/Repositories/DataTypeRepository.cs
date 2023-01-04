using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Repositories
{
    /// <summary>
    /// An implementation of the contract <seealso cref="IDataTypeRepository"/>.
    /// </summary>
    internal class DataTypeRepository : IDataTypeRepository
    {
        private readonly Context context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTypeRepository"/> class.
        /// </summary>
        /// <param name="context"><seealso cref="Context"/></param>
        public DataTypeRepository(Context context)
        {
            this.context = context;
        }

        public List<DataType> GetAll()
        {
            List<DataType> result = context.DataTypes
                .ToList();

            return result;
        }
    }
}
