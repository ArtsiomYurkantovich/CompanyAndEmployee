using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]

        public string Surname { get; set; }
        [Display(Name = "Middle name")]
        [Required(ErrorMessage = "Middle Name is required.")]

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Employment date is required.")]
        [Display(Name = "Date of employment")]
        public DateTime GetJob { get; set; }
        [Required(ErrorMessage = "Position employee is required.")]

        public string Position { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
