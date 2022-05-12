using Dapper;
using System;
using System.Data;

namespace Restaurant.Infrastructure.Mappings
{
    internal class SqliteGuidHandler : SqlMapper.TypeHandler<Guid>
    {
        public override void SetValue(IDbDataParameter parameter, Guid guid)
        {
            parameter.Value = guid.ToString();
        }

        public override Guid Parse(object value)
        {
            var guid = new Guid((string)value);
            return guid;
        }
    }
}
