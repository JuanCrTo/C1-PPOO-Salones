namespace Salones.Models
{
    public class EquipoTecnologico
    {
        public int Id { get; set; }
        public string Nombre { get; set; } // Ejemplo: "Proyector", "PC"

        // Relación muchos a uno con Salon
        public int SalonId { get; set; }
        public Salon Salon { get; set; }
    }

}
