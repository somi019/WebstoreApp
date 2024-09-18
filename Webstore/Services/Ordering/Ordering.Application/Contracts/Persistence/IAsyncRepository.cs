using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : AggregateRoot
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T,bool>> predicate);
        // kao argument saljemo funkciju koja prima T i vraca bool
        // da bi koristili kao predikat u izrazima moramo da koristimo Expression
        // nejasno skroz, pogledati sta su tacno : DELEGATI I LINQ IZRAZI
        // DELEGAT- POKAZIVAC NA FUNKCIJU, LINQ - ZA BARATANJE SA LISTAMA

        Task<IReadOnlyList<T>> GetAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string? includeString = null,
            bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(
            Expression<Func<T,bool>> predicate = null,
            Func<IQueryable<T>,IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T,object>>> includes = null,
            bool disableTracking = true);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);


    }
}

// napisali smo ovako opsti interfejs, da kada budemo implementirali interfejs za neki
// konkretan agregat, mi cemo moci da taj interfejs koji budemo pisali NASLEDI OPSTI

