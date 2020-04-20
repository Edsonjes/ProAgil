using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;
        public ProAgilRepository(ProAgilContext context)
        {
             _context = context;
             _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        //TODAS AS ENTIDADES
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

       

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

          public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        
        //EVENTOS

        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedesSociais);

            if(includePalestrante)
            {
                query = query.Include(Pe => Pe.PalestranteEventos).ThenInclude(P => P.Palestrante);


            }
            query = query.OrderBy(c => c.Id);
            
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrante = false)
        {
             IQueryable<Evento>  query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c =>c.RedesSociais);

            if(includePalestrante)
            {
                query = query
                .Include(pe => pe.PalestranteEventos)
                .ThenInclude(p => p.Palestrante);

            }
            query = query.AsNoTracking().OrderBy(c => c.Id)
            .Where(c => c.Tema.Contains(tema));
            
            return await query.ToArrayAsync();
        }

         public async Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrante = false)
        {
              IQueryable<Evento>  query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c =>c.RedesSociais);

            if(includePalestrante)
            {
                query = query
                .Include(pe => pe.PalestranteEventos)
                .ThenInclude(p => p.Palestrante);

            }
            query = query.AsNoTracking().OrderBy(c => c.Id)
            .Where(c => c.Id ==EventoId);
            
            return await query.FirstOrDefaultAsync();
        }
        //PALSTRANTE

        public async Task<Palestrante> GetAllPalestranteAsync(int PalestranteId, bool includeEventos = false)
        {
             IQueryable<Palestrante>  query = _context.Palestrante
            
            .Include(c =>c.RedesSociais);

            if(includeEventos)
            {
                query = query
                .Include(pe => pe.PalestranteEventos)
                .ThenInclude(e => e.Eventos);

            }
            query = query.AsNoTracking().OrderBy(p => p.Nome).Where(p => p.Id == PalestranteId);
            
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsyncByName(string name, bool includeEventos = false)
        {
           IQueryable<Palestrante>  query = _context.Palestrante
            
            .Include(c =>c.RedesSociais);

            if(includeEventos)
            {
                query = query
                .Include(pe => pe.PalestranteEventos)
                .ThenInclude(e => e.Eventos);

            }
            query = query.AsNoTracking().OrderBy(p => p.Nome.ToLower().Contains(name.ToLower()));
            
            
            return await query.ToArrayAsync();
        }

     
    }
}