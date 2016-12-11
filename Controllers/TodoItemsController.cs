using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domaca_Zadaca_3.Core.Models;
using Domaca_Zadaca_3.Data;
using Domaca_Zadaca_3.Core.Interface;
using Microsoft.AspNetCore.Identity;
using Domaca_Zadaca_3.Models;
using Microsoft.AspNetCore.Authorization;

namespace Domaca_Zadaca_3.Controllers
{
    public class TodoItemsController : Controller
    {
        private readonly ITodoRepository _repository;//konvencija za private
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoItemsController(ITodoRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        // GET: TodoItems
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUser();

            return View(_repository.GetActive(Guid.Parse(user.Id)));
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(TodoView viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUser();
                var item = new TodoItem(viewModel.Text, Guid.Parse(user.Id));
                _repository.Add(item);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Completed()
        {
            var user = await GetCurrentUser();
            return View(_repository.GetCompleted(Guid.Parse(user.Id)));
        }

        public async Task<IActionResult> MarkCompleted(Guid id)
        {
            var user = await GetCurrentUser();
            _repository.MarkAsCompleted(id, Guid.Parse(user.Id));
            return RedirectToAction("Index");
        }
    }
}
