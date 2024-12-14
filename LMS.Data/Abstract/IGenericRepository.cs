
namespace LMS.Data.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        //-------CRUD-------------------
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAnsyc(T etity);
        Task<bool> DeleteAsync(T entity);

        //-------Tracking---------------

        IQueryable<T> GetTableAsTracking();
        IQueryable<T> GetTableNoTracking();
        //----------Transcation----------
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollBackAsync();




    }
}
