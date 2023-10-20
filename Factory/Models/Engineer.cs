using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Factory.Models
{
    public class Engineer
    {
        public int EngineerId { get; set; }
        [Required(ErrorMessage = "The engineer's name cannot be empty!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter the hire date.")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        public List<EngineerMachine> JoinEntities { get; set; }
    }
}