using System;
using System.Collections.Generic;

#nullable disable

namespace ListaDeTarefas.Models
{
    public partial class Tarefa
    {
        public int TarefasId { get; set; }
        public int? FkUserTarefa { get; set; }
        public string NomeTarefa { get; set; }
        public string Descricao { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public int Situacao { get; set; }
        public int Prioridade { get; set; }

        public string DescricaoStatus => Situacao == 1 ? "Ativo" : "Inativo";

        public string DescricaoPrioridade => Prioridade == 1 ? "Alta" : "Baixa";


        public virtual User FkUserTarefaNavigation { get; set; }
    }
}
