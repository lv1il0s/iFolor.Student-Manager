using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iFolor.StudentManager.Core.Contracts.Infrastructure
{
    /// <summary>
    /// Generic repository interface for basic CRUD operations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Get an item by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Item.</returns>
        T GetById(uint id);

        /// <summary>
        /// Get all items.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Add an item.
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);

        /// <summary>
        /// Remove an item by its ID.
        /// </summary>
        /// <param name="id"></param>
        void Remove(int id);

        /// <summary>
        /// Save changes.
        /// </summary>
        /// <returns></returns>
        string SaveChanges();
    }
}
