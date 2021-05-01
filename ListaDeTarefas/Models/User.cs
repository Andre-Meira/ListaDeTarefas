using System;
using System.Collections.Generic;

#nullable disable

namespace ListaDeTarefas.Models
{
    public partial class User
    {
        public User()
        {
            Tarefas = new HashSet<Tarefa>();
        }

        public int IdUser { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }

        public virtual ICollection<Tarefa> Tarefas { get; set; }
    }
}
