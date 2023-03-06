using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univerus.Console.Domain.Entities
{
    public class PersonType
    {
        [Key]
        public int PersonTypeId { get; set; }
        public string Description { get; set; }
    }
}
