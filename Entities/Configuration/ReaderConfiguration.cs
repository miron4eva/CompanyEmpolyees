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
    public class ReaderConfiguration : IEntityTypeConfiguration<Reader>
    {
        public void Configure(EntityTypeBuilder<Reader> builder)
        {
            builder.HasData
            (
            new Reader
            {
                Id = new Guid("54d10fcf-81f9-4771-b8ca-b6c090cae604"),
                Name = "Irina Belaya",
                Age = 24,
                LibraryId = new Guid("83797f46-00b7-41d6-b61c-0a8a8a013cc5")
            },
            new Reader
            {
                Id = new Guid("50fe9ec9-094c-466b-b5c0-9a6486654289"),
                Name = "Sasha Cherniy",
                Age = 31,
                LibraryId = new Guid("e918df18-c597-4e14-a0da-7bbfb649122c")
            },
            new Reader
            {
                Id = new Guid("dffcc5d0-1368-429a-95e6-76ac034cd910"),
                Name = "Lika Seraya",
                Age = 18,
                LibraryId = new Guid("83797f46-00b7-41d6-b61c-0a8a8a013cc5")
            }
            );

        }
    }
}
