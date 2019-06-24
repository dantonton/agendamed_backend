using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agendamed_backend.Domain.Results
{
    public class Erro
    {
        public String Property { get; set; }
        public String CodeErro { get; set; }
        public String Message { get; set; }
        public String AdminMessage { get; set; }
    }
}
