using agendamed_backend.Domain.Entities;
using agendamed_backend.Domain.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace agendamed_backend.Data.Repositories
{
    public class CompromissoRepository : IDisposable
    {
        private AgendaDbContext Db;
        private DbSet<Compromisso> DbSet;

        public CompromissoRepository(AgendaDbContext db)
        {
            Db = db;
            DbSet = db.Set<Compromisso>();
        }


        public async Task<Result<Compromisso>> AddAsync(Compromisso obj)
        {
            var r = new Result<Compromisso>(obj);
            obj.Registro = DateTimeOffset.Now;
            try
            {

                await DbSet.AddAsync(obj);
                await Db.SaveChangesAsync();
                return r;
            }
            catch (DbUpdateException e)
            {
                r.AddErro("all", "db", "Erro ao tentar acessar o banco de dados", e.Message);
                return r;
            };
        }

        public async Task<Compromisso> GetByIDAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }
        

        public async Task<Result<List<Compromisso>>> GetAllAsync()
        {
            return new Result<List<Compromisso>>(
                await DbSet.OrderBy(x=>x.Inicio).ToListAsync()
                );
            
        }


        public void Dispose()
        {
            Db.Dispose();
        }

        internal async Task<bool> OtherByRangeDateAsync(Guid id, DateTimeOffset inicio, DateTimeOffset fim)
        {
            return await DbSet.AnyAsync(x => x.ID != id && x.Fim > inicio && x.Inicio <= fim);
        }

        public async Task<Result<Compromisso>> UpdateAsync(Compromisso obj)
        {

            var r = new Result<Compromisso>(obj);
            try
            {

                Db.Entry(obj).State = EntityState.Modified;
                await Db.SaveChangesAsync();
                return r;
            }
            catch (DbUpdateException e)
            {
                r.AddErro("all", "db", "Erro ao tentar acessar o banco de dados", e.Message);
                return r;
            };

            
        }
        public async Task<Result<Compromisso>> DeleteAsync(Compromisso obj)
        {

            var r = new Result<Compromisso>(obj);
            try
            {

                DbSet.Remove(obj);
                await Db.SaveChangesAsync();
                return r;
            }
            catch (DbUpdateException e)
            {
                r.AddErro("all", "db", "Erro ao tentar acessar o banco de dados", e.Message);
                return r;
            };


        }
    }
}
