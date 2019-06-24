using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agendamed_backend.Domain.Entities;

namespace agendamed_backend.Models
{
    public class CompromissoMV
    {
        
        public string paciente { get; set; }
        public string nascimento { get; set; }
        public string inicio { get; set; }
        public string fim { get; set; }
        public string observacao { get; set; }

        internal Compromisso ToModel()
        {
            DateTimeOffset dd = DateTimeOffset.Parse(inicio);
            
            throw new NotImplementedException();
        }
    }
}
