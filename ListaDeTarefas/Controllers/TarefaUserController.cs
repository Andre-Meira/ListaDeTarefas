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
        [HttpGet("/TarefaUser/{id}")]
        public IActionResult Index(int id, int? situacao)
        {
            var TarefaPorUsuario = _context.Tarefas.Where(t => t.FkUserTarefa == id);
            ViewData["id"] = id;
            if (situacao != null)
            {
               
                return View(_context.Tarefas.Where(x => x.Situacao == situacao));
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

    }
}

