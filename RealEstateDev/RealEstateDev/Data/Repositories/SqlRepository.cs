﻿using Microsoft.EntityFrameworkCore;
using RealEstateDev.Data;
using RealEstateDev.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDev.Repositories
{
    public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
    {

        private readonly DbSet<T> _dbSet;
        private readonly DbContext _dbContext;

        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;

        public SqlRepository(RealEstateDevDbContex dbContex)
        {
            _dbContext = dbContex;
            _dbSet = _dbContext.Set<T>();
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public void Add(T item)
        {
            _dbSet.Add(item);
            ItemAdded.Invoke(this, item);
        }
        public void Remove(T item)
        {
            _dbSet.Remove(item);
            ItemRemoved.Invoke(this, item);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}