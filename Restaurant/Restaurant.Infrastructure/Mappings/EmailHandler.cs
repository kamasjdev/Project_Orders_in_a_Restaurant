using Dapper;
using Restaurant.Domain.Entities;
using System.Data;

namespace Restaurant.Infrastructure.Mappings
{
    internal sealed class EmailHandler : SqlMapper.TypeHandler<Email>
    {
        public override Email Parse(object value)
        {
            return Email.Of((string)value);
        }

        public override void SetValue(IDbDataParameter parameter, Email value)
        {
            parameter.Value = value.ToString();
        }
    }
}
