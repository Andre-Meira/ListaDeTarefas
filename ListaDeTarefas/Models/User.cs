using System.Collections.Generic;

namespace ListaDeTarefas.Models
{
	public partial class User
	{
		public int IdUser { get; set; }
		public string Usuario { get; set; }
		public string Senha { get; set; }

		public virtual ICollection<Tarefa> Tarefas { get; set; }
	}
}
