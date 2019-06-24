using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agendamed_backend.Domain.Entities
{
    public class Compromisso
    {
        public Guid ID { get; set; }
        public String Nome { get; set; }
        public DateTimeOffset Nascimento { get; set; }
        public DateTimeOffset Inicio { get; set; }
        public DateTimeOffset Fim { get; set; }
        public String Observacao { get; set; }
        public DateTimeOffset Registro { get; set; }

        public static Compromisso New()
        {
            return new Compromisso()
            {
                ID = Guid.NewGuid()
            };
        }
    }
}
