namespace Agenda.WebUI.Models
{
    public class DDDModel
    {
        public int Ddd { get; set; }
        public string Descricao { get; set; }

        public DDDModel(int ddd, string descricao)
        {
            Ddd = ddd;
            Descricao = descricao;
        }
    }
}
