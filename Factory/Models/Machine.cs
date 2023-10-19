using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Factory.Models
{
    public class Machine
    {
        public int MachineId { get; set; }
        [Required(ErrorMessage = "The machine's description cannot be empty!")]
        public string Description { get; set; }
        public List<EngineerMachine> JoinEntities { get; set; }
    }
}