using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueVistoHoje.API.Data;

namespace QueVistoHoje.API.Entities
{
    public class Registo
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public ApplicationUser Utilizador { get; set; }
    }
}
