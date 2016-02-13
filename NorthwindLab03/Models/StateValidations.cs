using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NorthwindLab03.Models
{
    [MetadataType(typeof(StateValidation))]
    public partial class State
    {

    }
    public class StateValidation
    {
        [Required]
        public string statename { get; set; }
        [Required]
        public string description { get; set; }
    }
}