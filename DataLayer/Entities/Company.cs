using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public int Size { get; set; }

        [Required(ErrorMessage = "Form Of Organization is required.")]
        [Display(Name = "Form Of Organization")]
        public string FormOfOrganization { get; set; }

        public virtual List<Employee> Employees { get; set; }
    }
}
