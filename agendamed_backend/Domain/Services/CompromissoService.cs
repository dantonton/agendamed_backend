using agendamed_backend.Data.Repositories;
using agendamed_backend.Domain.Entities;
using agendamed_backend.Domain.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agendamed_backend.Domain.Services
{
    public class CompromissoService
    {
        private readonly CompromissoRepository compromissoRepository;

        public CompromissoService(CompromissoRepository compromissoRepository)
        {
            this.compromissoRepository = compromissoRepository;
        }

        public async Task<Result<List<Compromisso>>> GetAllAsync()
        {

            return await compromissoRepository.GetAllAsync();
        }

        public async Task<Result<Compromisso>> GetByIDAsync(Guid id)
        {
            var c = await compromissoRepository.GetByIDAsync(id);
            var r = new Result<Compromisso>(c);
            if (c == null)
            {
                
                r.AddErro("all", "null", "Nenhum compromisso encontrado com este identificador");
                
            }

            return r;
        }

        public async Task<Result<Compromisso>> CreateAsync(Compromisso c)
        {
            Result<Compromisso> r = await Validate(c);

            if (r.Type == ResultType.erro)
                return r;
            r.Join(await compromissoRepository.AddAsync(c));

            return r;
        }


        public async Task<Result<Compromisso>> UpdateAsync(Compromisso compromisso)
        {
            Result<Compromisso> r = await GetByIDAsync(compromisso.ID);
            if (r.Type == ResultType.erro)
                return r;
            r.Body.Nome = compromisso.Nome;
            r.Body.Nascimento = compromisso.Nascimento;
            r.Body.Inicio = compromisso.Inicio;
            r.Body.Fim = compromisso.Fim;
            r.Body.Observacao = compromisso.Observacao;

            r.Join(await Validate(r.Body));

            if (r.Type == ResultType.erro)
                return r;
            r.Join(await compromissoRepository.UpdateAsync(r.Body));

            return r;
        }

        public async Task<Result<Compromisso>> DeleteAsync(Guid id)
        {
            Result<Compromisso> r = await GetByIDAsync(id);
            if (r.Type == ResultType.erro)
                return r;
            
            r.Join(await compromissoRepository.DeleteAsync(r.Body));

            return r;
        }


        private async Task<Result<Compromisso>> Validate(Compromisso c)
        {
            var r = new Result<Compromisso>(c);

            if (String.IsNullOrWhiteSpace(c.Nome))
                r.AddErro("Nome", "required", "Nome é obrigatório");
            else if (c.Nome.Length >= 255)
                r.AddErro("Nome", "maxlength", "Nome deve conter no máximo 255 caracteres");
            if (c.Fim <= c.Inicio)
                r.AddErro("Fim", "minTo", "Data Fim não pode ser menor ou igual a data de início");
            else if (await compromissoRepository.OtherByRangeDateAsync(c.ID, c.Inicio, c.Fim))
                r.AddErro("InicioFim", "busydate", "Já existe compromisso marcado nesse intervalo de tempo");
            if (c.Observacao.Length >= 5000)
                r.AddErro("Observacao", "maxlength", "Observação deve conter no máximo 5000 caracteres");
            return r;
        }

        
    }
}
