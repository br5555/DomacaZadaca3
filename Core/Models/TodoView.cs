using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domaca_Zadaca_3.Core.Models
{
    public class TodoView
    {
        [Required]
        [MaxLength(150)]
        public string Text { get; set; }
    }
}
