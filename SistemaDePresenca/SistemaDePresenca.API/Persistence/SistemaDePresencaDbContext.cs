using SistemaDePresenca.API.Entities;

namespace SistemaDePresenca.API.Persistence
{
    public class SistemaDePresencaDbContext
    {
        public List<Materia> Materias { get; set; }

        public SistemaDePresencaDbContext()
        {
            Materias = new List<Materia>();
        }
    }
}
