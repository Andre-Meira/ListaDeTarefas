using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ListaDeTarefas.Models;

namespace ListaDeTarefas.Controllers
{
    public class UserController : Controller
    {
        private readonly ListaTarefaDBContext _context;
        private readonly CryptographyHash _cryptography;

        public UserController(ListaTarefaDBContext context, CryptographyHash cryptography)
        {   
            _context = context;
            _cryptography = cryptography;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/User/VerificaUser/{UserName?}/{Senha?}")]
        public IActionResult VerificaUser(string UserName, string Senha)
        {
            if (!String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(Senha))
            {
                var UserChecked = CheckUserExist(UserName, Senha);
                if (UserChecked == null)
                    return View();
                return RedirectToAction("Index", "TarefaUser", new { id = UserChecked.IdUser });
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
                string[] getUserArray = {user.Usuario};
                string[] getSenhaArray = {user.Senha};
                bool isUserVerificed = isCheckUserPattern(getUserArray, getSenhaArray);
                User UserChecked = CheckUserExist(user.Usuario);

                if (!isUserVerificed )
                {
                    if (UserChecked == null)
                    {
                        _context.Entry(user).Property(u => u.Senha).CurrentValue = _cryptography.CriptografarSenha(user.Senha);
                        _context.Users.Add(user);
                        _context.SaveChanges();
                        return RedirectToAction("Index", "TarefaUser", new {id = user.IdUser});
                    }
                    else
                    { ViewBag.alerta = "Esse Usuario Ja Existe!!";}
                }
                else
                { ViewBag.alerta = "Erro!! Senha ou Usuario nao e valido!!"; }                                     
            }
            return View();
        }

        private User CheckUserExist(string name) 
        {
            return _context.Users.Where(x => x.Usuario == name).FirstOrDefault();
        }
        private User CheckUserExist(string name, string senha)
        {
            return _context.Users.Where(x => x.Usuario == name && x.Senha == senha).FirstOrDefault();
        }
        private bool isCheckUserPattern(string[] user, string[] senha) 
        {
            int MinOfWords = 3;
            bool isValidUser = Array.Exists(user, element => element.Length > MinOfWords);
            bool isValidPassword = Array.Exists(senha, element => element.Length > MinOfWords && element.StartsWith(element.ToUpper()));
            return isValidPassword == isValidUser;
        }     
    }
}
