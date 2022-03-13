using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Quotation.BuildingBlocks.Database.Abstractions;

namespace Database.Extensions
{
    public static class DbSetExtensions
    {
        //TODO: Техдолг

        /// <summary>
        /// Обновляет сущность в БД находят запись с помощью лямбда-функции.
        /// </summary>
        /// <typeparam name="TEntity">Сущность имеющая идентификатор.</typeparam>
        /// <param name="dbSet">Таблица БД.</param>
        /// <param name="context">Контекст БД.</param>
        /// <param name="identifier">Лямбда-функция для нахождения нужной записи на обновление.</param>
        /// <param name="entity">Сущность для обновления.</param>
        //public static TEntity AddOrUpdate<TEntity>(this DbSet<TEntity> dbSet, DbContext context, Func<TEntity, object> identifier, TEntity entity)
        //    where TEntity : class, IIdentifiable
        //{
        //    try
        //    {
        //        TEntity result = dbSet.Find(identifier.Invoke(entity));
        //        if (result != null)
        //        {
        //            context.Entry(result).CurrentValues.SetValues(entity);
        //            dbSet.Update(result);
        //            return result;
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        Console.WriteLine(exc);
        //    }

        //    return null;
        //}

        /// <summary>
        /// Обновляет сущность в БД находят запись с помощью LINQ вырадения.
        /// </summary>
        /// <typeparam name="TEntity">Сущность имеющая идентификатор.</typeparam>
        /// <param name="dbSet">Таблица БД.</param>
        /// <param name="context">Контекст БД.</param>
        /// <param name="predicate">LINQ вырадение для нахождения нужной записи на обновление.</param>
        /// <param name="entity">Сущность для обновления.</param>
        //public static TEntity Update<TEntity>(this DbSet<TEntity> dbSet, DbContext context, Expression<Func<TEntity, bool>> predicate, TEntity entity)
        //    where TEntity : class, IIdentifiable
        //{
        //    try
        //    {
        //        var result = dbSet.FirstOrDefault(predicate);
        //        if (result != null)
        //        {
        //            context.Entry(result).CurrentValues.SetValues(entity);
        //            dbSet.Update(result);
        //            return result;
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        Console.WriteLine(exc);
        //    }

        //    return null;
        //}
    }
}
