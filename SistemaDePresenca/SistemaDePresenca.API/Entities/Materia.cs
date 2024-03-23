namespace SistemaDePresenca.API.Entities
{
    public class Materia
    {
        public Materia()
        {
            EstaAtiva = true;
        }
        public Guid Id { get; set; }
        public string Nome { get; set; } 
        public int FrequenciaExigida { get; set; }
        public int NumeroDeFaltas { get; set; } // Numero das faltas até o dia de hoje.
        public int TotalDeAulas { get; set; }
        public bool EstaAtiva { get; set; }

        public void Update(string nome,
                           int frequenciaExigida,
                           int numeroDeFaltas,
                           int totalDeAulas,
                           bool estaAtiva)
        {
            this.Nome = nome;
            this.FrequenciaExigida = frequenciaExigida;
            this.NumeroDeFaltas = numeroDeFaltas;
            this.TotalDeAulas = totalDeAulas;
            this.EstaAtiva = estaAtiva;
        }

        public void Delete()
        {
            this.EstaAtiva = false;
        }
    }
}
