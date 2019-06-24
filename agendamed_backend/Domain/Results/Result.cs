using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agendamed_backend.Domain.Entities;

namespace agendamed_backend.Domain.Results
{
    public class Result <T> where T : class
    {
        public Result()
        {

        }
        public Result(T obj)
        {
            this.Body = obj;
        }

        public ResultType Type { get; set; } = ResultType.success;
        public T Body { get; set; }
        public List<Erro> Erros { get; set; } = new List<Erro>();

        internal void AddErro(string property, string codeErro, string message = "", string adminMessage = "")
        {
            Type = ResultType.erro;
            Erros.Add(new Erro()
            {
                Property = property,
                CodeErro = codeErro,
                Message = message,
                AdminMessage = adminMessage
            });
        }

        internal void Join(Result<T> otherResult)
        {
            if (otherResult.Type == ResultType.erro)
                Type = ResultType.erro;
            else
                if (Type == ResultType.success)
                Type = otherResult.Type;
            Erros.AddRange(otherResult.Erros);
        }
    }
}
