using Domaca_Zadaca_3.Core.Interface;
using Domaca_Zadaca_3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domaca_Zadaca_3.Core.Repositories
{
    class TodoSqlRepository : ITodoRepository
    {

        ///<summary>
        /// Gets TodoItem for a given id. Throw TodoAccessDeniedException with appropriate message if user is not the owner of the Todo item
        /// 
        /// </summary>
        /// <param name="todoId"> Todo Id</param>
        /// <param name="userId">Id of the user that is trying to fetch the data</param>
        /// <returns>TodoItem if found, null otherwise</returns>
        public TodoItem Get(Guid todoId, Guid userId)
        {
            using (var db = new TodoItemDbContext())
            {
                var privremeni = db.TodoItems.Where(s => s.Id == todoId).FirstOrDefault();
                if (privremeni == null)
                {
                    return null;
                }
                else if (privremeni.UserId == userId)
                {
                    return privremeni;
                }
                else
                {
                    throw new TodoAccessDeniedException();
                }
            }
        }



        ///<summary>
        ///Gets all incomplete TodoItem objects in database for user
        /// </summary>
        public List<TodoItem> GetActive(Guid userId)
        {
            using (var db = new TodoItemDbContext())
            {
                return db.TodoItems.Where(s => s.IsCompleted == false).Where(s => s.UserId == userId).ToList();
            }
        }
        ///<summary>
        ///Gets all TodoItem objects in database for user, sorted by date created(descending)
        /// </summary>
        public List<TodoItem> GetAll(Guid userId)
        {
            using (var db = new TodoItemDbContext())
            {
                return db.TodoItems.ToList();
            }
        }


        ///<summary>
        ///Gets all completed TodoItem objects in database for user
        /// </summary>
        public List<TodoItem> GetCompleted(Guid userId)
        {
            using (var db = new TodoItemDbContext())
            {
                return db.TodoItems.Where(s => s.IsCompleted == true).Where(s => s.UserId == userId).ToList();
            }
        }
        ///<summary>
        ///Gets all TodoItem objects in database for user that apply to the filter
        /// </summary>
        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            using (var db = new TodoItemDbContext())
            {
                return db.TodoItems.Where(s => s.UserId == userId).Where(filterFunction).ToList();
            }
        }
        ///<summary>
        ///Tries to mark a TodoItem as completed in database. Throw TodoAccessDeniedException with appropriate message if user is not the owner of the Todo item
        ///
        /// </summary>
        /// <param name="todoId">Todo Id</param>
        /// <param name="userId">Id of the user that is trying to mark as completed</param>
        /// <returns>True if success, false otherwise</returns>
        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            var privremeni = new TodoSqlRepository();
            var todoItemprivremeni = privremeni.Get(todoId, userId);
            if (todoItemprivremeni == null)
            {
                return false;
            }
            else
            {
                using (var db = new TodoItemDbContext())
                {
                    todoItemprivremeni.DateCompleted = DateTime.Now;
                    todoItemprivremeni.IsCompleted = true;
                    db.SaveChanges();
                    return true;
                }
            }
        }
        ///<summary>
        ///Updates given TodoItem in database
        ///If TodoItem does not exist, method will add one.Throw TodoAccessDeniedException with appropriate message if user is not the owner of the Todo item
        ///
        /// </summary>
        /// <param name="todoItem">Todo item </param>
        /// <param name="userId">Id of the user that is trying to update the data
        /// </param>
        public void Update(TodoItem todoItem, Guid userId)
        {
            var privremeni = new TodoSqlRepository();
            var todoItemprivremeni = privremeni.Get(todoItem.Id, userId);
            if (todoItemprivremeni == null)
            {
                using (var db = new TodoItemDbContext())
                {

                    db.TodoItems.Add(todoItem);
                    db.SaveChanges();
                }
            }
        }
    }
}
