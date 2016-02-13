using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NorthwindLab03.Models
{
    [MetadataType(typeof(PersonalValidation))]
    public partial class Personal
    {

    }
    public class PersonalValidation
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string apellidop { get; set; }
        [Required]
        public string apellidom { get; set; }
        [Required]
        public string lugarnacimiento { get; set; }
        [Required]
        public string nacionalidad { get; set; }
        [Required]
        public string sexo { get; set; }
        [Required]
        public string estadocivil { get; set; }
    }
}