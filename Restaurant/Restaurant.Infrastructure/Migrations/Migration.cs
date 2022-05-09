using System;

namespace Restaurant.Infrastructure.Migrations
{
    internal class Migration
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }
}
