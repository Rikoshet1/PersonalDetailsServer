﻿using Microsoft.EntityFrameworkCore;
using MyProject.Repositories.Entities;
using MyProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Repositories.Repositories
{
    public class ChildRepository : IChildRepository
    {
        readonly IContext _context;

        public ChildRepository(IContext context)
        {
            _context= context;
        }

        public async Task<Child> AddAsync(string name, DateTime date, string tz, int parentId)
        {
            var newChild = new Child
            {
              Name=name,
                DateOfBirth = date,
                Tz = tz,
               ParentId= parentId
            };
            await _context.Children.AddAsync(newChild);
            await _context.SaveChangesAsync();
            return newChild;
        }

        public async Task DeleteAsync(int id)
        {
            var child = await GetByIdAsync(id);
            _context.Children.Remove(child);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Child>> GetAllAsync()
        {
            return await _context.Children.ToListAsync();
        }

        public async Task<Child> GetByIdAsync(int id)
        {
            return await _context.Children.FindAsync(id);
        }

        public async Task<Child> UpdateAsync(Child child)
        {
            var updatedChild = _context.Children.Update(child);
            await _context.SaveChangesAsync();
            return updatedChild.Entity;
        }
    }
}
