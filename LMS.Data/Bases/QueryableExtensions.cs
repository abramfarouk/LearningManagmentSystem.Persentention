﻿namespace LMS.Data.Bases
{
    public static class QueryableExtensions
    {
        public static async Task<PigatedResult<T>> ToPaginatedListAsync<T>
            (this IQueryable<T> source, int pageNumber, int pageSize) where T : class
        {
            if (source == null)
                throw new ArgumentNullException("Empty");

            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int count = await source.AsNoTracking().CountAsync();
            if (count == 0)
            {
                return PigatedResult<T>.Success(new List<T>(), count, pageNumber, pageSize);
            }

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return PigatedResult<T>.Success(items, count, pageNumber, pageSize);
        }
    }
}
