namespace Salones.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; } // Ejemplo: 2024-11-18 10:00 AM
        public DateTime FechaFin { get; set; } // Ejemplo: 2024-11-18 12:00 PM
        public string Estado { get; set; } // Ejemplo: "Pendiente", "Confirmada"

        // Relación muchos a uno con Usuario
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        // Relación muchos a uno con Salon
        public int SalonId { get; set; }
        public Salon Salon { get; set; }
    }

}
