using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaDeTarefas.Models;
using System.Diagnostics;

namespace ListaDeTarefas.Controllers
{
    public class UserController : Controller
    {

        private readonly ListaTarefaDBContext _context;
        public UserController(ListaTarefaDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/User/VerificaUser/{UserName?}/{Senha?}")]
        //Faz a Vereficação se Valido Redireciona a Action passando o paramentro ID
        public IActionResult VerificaUser(string UserName, string Senha) 
        {
            if (!String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(Senha)) 
            {
                var validUser = _context.Users.Where(x => x.Usuario == UserName && x.Senha == Senha).FirstOrDefault();
                if (validUser == null)
                {
                    ViewBag.alerta = "Erro";
                    return View();
                }             
                return RedirectToAction("Index", "TarefaUser", new { id = validUser.IdUser });
            }          
            return View();
        }

        [HttpGet]
        public IActionResult NovoUser() 
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NovoUser(User user) 
        {
            if (ModelState.IsValid) 
            {
                var verificaUserExiste = _context.Users.Where(u => u.Usuario == user.Usuario).FirstOrDefault();
                if (verificaUserExiste == null) 
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return RedirectToAction("index", "User");
                }
                ViewBag.alerta = "Esse Usuario Ja Existe!!";
                return View();
            }
            return View();
        }

    
    }
}
