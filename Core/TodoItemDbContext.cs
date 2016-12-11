using Domaca_Zadaca_3.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Domaca_Zadaca_3.Core
{
    public class TodoItemDbContext : DbContext
    {
        public IDbSet<TodoItem> TodoItems { get; set; }

        public TodoItemDbContext() : base("Server=(localdb)\\mssqllocaldb;Database=TodoItemDb;Trusted_Connection=True;MultipleActiveResultSets=true")
        {

        }

        public TodoItemDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<TodoItem>().HasKey(c => c.Id);
        //    modelBuilder.Entity<TodoItem>().Property(c => c.DateCreated).IsRequired();
        //    modelBuilder.Entity<TodoItem>().Property(c => c.DateCompleted).IsRequired();
        //    modelBuilder.Entity<TodoItem>().Property(c => c.IsCompleted).IsRequired();
        //    modelBuilder.Entity<TodoItem>().Property(c => c.Text).IsRequired();
        //    modelBuilder.Entity<TodoItem>().Property(c => c.UserId).IsRequired();




        //}


    }
}
