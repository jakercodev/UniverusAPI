using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univerus.Console.Domain.DTO
{
    public class PersonDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int PersonTypeId { get; set; }
        public string PersonType { get; set; }

    }
}
