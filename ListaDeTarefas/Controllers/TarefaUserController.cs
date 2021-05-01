using ListaDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaDeTarefas.Controllers;

namespace ListaDeTarefas.Controllers
{
    public class TarefaUserController : Controller
    {
        private readonly ListaTarefaDBContext _context;
        public TarefaUserController(ListaTarefaDBContext context)
        {
            _context = context;
        }
        [HttpGet("/TarefaUser/{id}/{situacao?}")]
        public IActionResult Index(int id, int? situacao)
        {
            var TarefaPorUsuario = _context.Tarefas.Where(t => t.FkUserTarefa == id);
            ViewData["id"] = id;
            ViewData["User"] = _context.Users.Find(id).Usuario;
            if (situacao != null)
            {               
                return View(_context.Tarefas.Where(x => x.Situacao == situacao && x.FkUserTarefa == id));
            }
            return View(TarefaPorUsuario) ;
        }

        [HttpGet("/TarefaUser/NovaTarefa/{id}")]
        public IActionResult NovaTarefa(int id)
        {
            ViewData["id"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NovaTarefa(int id, Tarefa tarefa) 
        {
            if (ModelState.IsValid) 
            {
                _context.Tarefas.Add(tarefa);
                _context.SaveChanges();
                return RedirectToAction("Index", new{id});
            }
            return NotFound();
        }


        [HttpGet("/TarefaUser/AtualizarTarefas/{id?}/{idTarefa?}")]
        public IActionResult AtualizarTarefas(int id, int idTarefa) 
        {

            var tarefa = _context.Tarefas.Where(t => t.TarefasId == idTarefa && t.FkUserTarefa == id).FirstOrDefault();
            ViewData["idTarefa"] = tarefa.TarefasId;
            return View(tarefa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AtualizarTarefas(int id, int idTarefa, Tarefa tarefa) 
        {
            if (ModelState.IsValid)
            {
                _context.Tarefas.Update(tarefa);
                _context.SaveChanges();
                return RedirectToAction("Index", new { id });
            }
            return View();
        }


		[HttpGet("/TarefaUser/ExcluirTarefa/{id}/{idTarefa}")]
		public IActionResult ExcluirTarefa(int id, int idTarefa)
		{
			var confirm = _context.Tarefas.FirstOrDefault(x => x.TarefasId == idTarefa && x.FkUserTarefa == id);
            ViewData["idTarefa"] = confirm.TarefasId;
			return View(confirm);
		}


		[HttpPost, ActionName("ExcluirTarefa")]
		[ValidateAntiForgeryToken]
		public IActionResult ConfirmExclusaoTarefa(int id, int idTarefa)
		{

			if (ModelState.IsValid)
			{
                var confirm = _context.Tarefas.FirstOrDefault(x => x.TarefasId == idTarefa && x.FkUserTarefa == id);

                if (confirm != null)
				{
					_context.Tarefas.Remove(confirm);
					_context.SaveChanges();
					return RedirectToAction("Index", new { id });
                }        
				return RedirectToAction("Index", new { id });
			}
            return NotFound();
		}

	}
}

