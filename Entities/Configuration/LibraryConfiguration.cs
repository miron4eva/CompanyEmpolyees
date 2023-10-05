using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class LibraryConfiguration : IEntityTypeConfiguration<Library>
    {
        public void Configure(EntityTypeBuilder<Library> builder)
        {
            builder.HasData
            (
            new Library
            {
                Id = new Guid("83797f46-00b7-41d6-b61c-0a8a8a013cc5"),
                Name = "Biblioteka 1",
                Address = "Kakoy-to tam address",
            },
            new Library
            {
                Id = new Guid("e918df18-c597-4e14-a0da-7bbfb649122c"),
                Name = "Biblioteka 2",
                Address = "Drugoy kakoy-to tam address",
            }
            );
        }
    }
}
