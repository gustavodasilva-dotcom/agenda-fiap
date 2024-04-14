using Agenda.FIAP.Api.Domain.Entities;
using Infrastructure.Data.Context;
using Infrastructure.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ContatoRepository : BaseRepository<Contato>, IContatoRepository
    {
        private readonly DataContext dataContext;

        public ContatoRepository(DataContext dataContext)
           : base(dataContext) {
            this.dataContext = dataContext;
        }

        public Contato ObterPorId(int id) {
            return dataContext.Contato
                              .Where(a => a.Id == id).FirstOrDefault();
        }
    }
}
