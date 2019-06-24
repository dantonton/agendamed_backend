using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using agendamed_backend.Data;
using agendamed_backend.Data.Repositories;
using agendamed_backend.Domain.Entities;
using agendamed_backend.Domain.Results;
using agendamed_backend.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace backend.Test
{
    public class CompromissoCRUD
    {
        private readonly CompromissoService compromissoService;
        public CompromissoCRUD()
        {
            var services = new ServiceCollection();
            services.AddDbContextPool<AgendaDbContext>(options =>
                 options.UseSqlServer("Data Source=SQL5041.site4now.net;Initial Catalog=DB_A198EE_agendamed;User Id=DB_A198EE_agendamed_admin;Password=agenda2019;"
                )
            );
            services.AddScoped<CompromissoService>();
            services.AddScoped<CompromissoRepository>();

            var serviceProvider = services.BuildServiceProvider();
            compromissoService = serviceProvider.GetRequiredService<CompromissoService>();

        }
        [Fact]
        public async Task CreateExemplo()
        {
            var c = Compromisso.New();
            c.Nome = "Fulano";
            c.Nascimento = new DateTimeOffset(1988,6,20,0,0,0, new TimeSpan(-3, 0, 0));
            c.Inicio = DateTimeOffset.Now;
            c.Fim = c.Inicio.AddMinutes(1);
            c.Observacao = c.ID.ToString();

            var r = await compromissoService.CreateAsync(c);

            Assert.Equal(ResultType.success, r.Type);


        }

        [Fact]
        public async Task CreateUpdateDelete()
        {
            var c1 = Compromisso.New();
            c1.Nome = "Fulano";
            c1.Nascimento = new DateTimeOffset(1988, 6, 20, 0, 0, 0, new TimeSpan(-3, 0, 0));
            c1.Inicio = DateTimeOffset.Now.AddYears(100);
            c1.Fim = c1.Inicio.AddMinutes(1);
            c1.Observacao = c1.ID.ToString();

            var r1 = await compromissoService.CreateAsync(c1);

            Assert.Equal(ResultType.success, r1.Type);

         
            var c2 = new Compromisso();
            c2.ID = c1.ID;
            c2.Nome = "Ciclano";
            c2.Nascimento = new DateTimeOffset(1982, 6, 20, 0, 0, 0, new TimeSpan(-3, 0, 0));
            c2.Inicio = c1.Inicio;
            c2.Fim = c2.Inicio.AddSeconds(30);
            c2.Observacao = "mudou";

            var r2 = await compromissoService.UpdateAsync(c2);

            Assert.Equal(ResultType.success, r2.Type);

            var r3 = await compromissoService.DeleteAsync(c1.ID);



        }

        /// <summary>
        /// O sistema não deve permitir o agendamento de duas ou mais 
        /// consultas no mesmo range de datas.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ErroCompromissoMesmaData()
        {
            var c1 = Compromisso.New();
            c1.Nome = "Beltrano 1";
            c1.Nascimento = new DateTimeOffset(1988, 6, 20, 0, 0, 0, new TimeSpan(-3, 0, 0));
            c1.Inicio = DateTimeOffset.Now.AddYears(200);
            c1.Fim = c1.Inicio.AddMinutes(20);
            c1.Observacao = c1.ID.ToString();

            var r1 = await compromissoService.CreateAsync(c1);

            Assert.Equal(ResultType.success, r1.Type);


            var c2 = new Compromisso();
            c2.Nome = "Beltrano 2";
            c2.Nascimento = new DateTimeOffset(1982, 6, 20, 0, 0, 0, new TimeSpan(-3, 0, 0));
            c2.Inicio = c1.Inicio.AddMinutes(10);
            c2.Fim = c2.Inicio.AddMinutes(30);
            c2.Observacao = "---";

            var r2 = await compromissoService.CreateAsync(c2);

            Assert.Equal(ResultType.erro, r2.Type);
            Assert.Contains(r2.Erros, x =>x.CodeErro == "busydate");
            
        }

        /// <summary>
        /// O sistema não deve permitir que a
        /// data final não pode ser menor que a data inicial.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ErroInicioMaiorFim()
        {
            var c1 = Compromisso.New();
            c1.Nome = "Beltrano 1";
            c1.Nascimento = new DateTimeOffset(1988, 6, 20, 0, 0, 0, new TimeSpan(-3, 0, 0));
            c1.Inicio = DateTimeOffset.Now.AddYears(300);
            c1.Fim = c1.Inicio.AddMinutes(-20);
            c1.Observacao = c1.ID.ToString();

            var r1 = await compromissoService.CreateAsync(c1);

            Assert.Equal(ResultType.erro, r1.Type);
            Assert.Contains(r1.Erros, x => x.CodeErro == "minTo");

        }
    }
}
