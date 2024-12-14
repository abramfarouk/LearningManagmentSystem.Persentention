
namespace LMS.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {


        #region Fields
        private readonly ApplicationDbcontext _context;
        #endregion

        #region Ctor 
        public GenericRepository(ApplicationDbcontext context)
        {
            _context = context;
        }
        #endregion

        #region Functions 
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);

        }
        public async Task<bool> AddAsync(T entity)
        {

            //_context.TablesName.Add(entity)
            await _context.Set<T>().AddAsync(entity);
            var effectedRow = await _context.SaveChangesAsync();
            if (effectedRow >= 1) return true;
            return false;
        }
        public async Task<bool> UpdateAnsyc(T etity)
        {
            _context.Set<T>().Update(etity);
            var effectedRow = await _context.SaveChangesAsync();
            if (effectedRow >= 1) return true;
            return false;
        }
        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            var effectedRow = await _context.SaveChangesAsync();
            if (effectedRow >= 1) return true;
            return false;
        }

        public IQueryable<T> GetTableAsTracking()
        {
            return _context.Set<T>().AsTracking().AsQueryable();
        }
        public IQueryable<T> GetTableNoTracking()
        {
            return _context.Set<T>().AsNoTracking().AsQueryable();
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }
        public async Task RollBackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }




        #endregion
    }

}
