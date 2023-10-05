using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Reader
    {
        [Column("ReaderId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Reader name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Age is a required field.")]
        public int Age { get; set; }
        [ForeignKey(nameof(Library))]
        public Guid LibraryId { get; set; }
        public Library Library { get; set; }
    }
}
