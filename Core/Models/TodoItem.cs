using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domaca_Zadaca_3.Core.Models
{
    public class TodoItem
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Text { get; set; }
        
        public bool IsCompleted { get; set; }
        ///<summary>
        ///Nullable date time.
        ///DataTime is value type and won't allow nulls.
        ///DataTime? is nullable DateTime and will accept nulls.
        ///Use null when todo completed date does not exist(e.g. todo is still not completed)
        /// </summary>
        [Required]
        public DateTime? DateCompleted { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }

        ///<summary>
        ///User id that owns this TodoItem
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        public TodoItem(string text, Guid userId)
        {
            Id = Guid.NewGuid(); // Generates new unique identifier
            Text = text;
            IsCompleted = false;
            DateCreated = DateTime.Now; // Set creation date as current time
            UserId = userId;
        }

        public TodoItem()
        {
            //entity framework needs this one
            //not for use :-)
        }
    }
}
