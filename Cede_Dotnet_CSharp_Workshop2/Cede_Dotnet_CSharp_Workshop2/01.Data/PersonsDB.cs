using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cede_Dotnet_CSharp_Workshop2._01.Data
{
    public class PersonsDB : ListaPersonas
    {
        public IList<ListaPersonas> ListaPersonas { get; set; }

        public int ID { get; set; }

    }
}
