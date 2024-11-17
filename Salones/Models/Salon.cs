namespace Salones.Models
{
    public class Salon
    {
        public int Id { get; set; }
        public string Nombre { get; set; } // Ejemplo: "Sala A"
        public int Capacidad { get; set; } // Ejemplo: 50 personas
        public string Ubicacion { get; set; } // Ejemplo: "Edificio B, Piso 3"

        // Relación uno a muchos con EquipoTecnologico
        public ICollection<EquipoTecnologico> EquiposTecnologicos { get; set; } = new List<EquipoTecnologico>();

        // Relación uno a muchos con Reserva
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }

}
