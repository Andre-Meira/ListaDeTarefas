using ListaDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeTarefas.Controllers
{
    public class TarefaUserController : Controller
    {
        private readonly ListaTarefaDBContext _context;

        public TarefaUserController(ListaTarefaDBContext context)
        {
            _context = context;
        }

        [HttpGet("/TarefaUser/{id}/{situacao?}/{prioridade?}")]
        public async  Task<IActionResult> Index(int id, int? situacao, int? prioridade)
        {
            var TarefaPorUsuario = _context.Tarefas.Where(t => t.FkUserTarefa == id);         
            if (situacao != null && prioridade != null)
            {       
                return View(_context.Tarefas.Where(
                    table => ((table.Prioridade == prioridade && table.Situacao == situacao) && (table.FkUserTarefa == id))));
            }
            if (situacao != null || prioridade != null)
            {
                return View(_context.Tarefas.Where(
                    table => ((table.Prioridade == prioridade || table.Situacao == situacao) && (table.FkUserTarefa == id))));
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
        public IActionResult AtualizarTarefas(int id, Tarefa tarefa) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Tarefas.Update(tarefa);
                    _context.SaveChanges();
                    return RedirectToAction("Index", new { id });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefaExist(tarefa.TarefasId))
                    {
                        return NotFound();
                    }
                    else 
                    {
                        throw;
                    }
                }                
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

        private bool TarefaExist(int id)
        {
            return _context.Tarefas.Any(e => e.TarefasId == id);
        }
    }
}

