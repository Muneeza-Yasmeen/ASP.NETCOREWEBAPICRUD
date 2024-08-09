using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NETCOREWEBAPICRUD.Context
{
    public class Taskss
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public int Priority { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("Name")]
        public Users User { get; set; }

        [ForeignKey("CategoryId")]
        public Categories Category { get; set; }
    }
}
