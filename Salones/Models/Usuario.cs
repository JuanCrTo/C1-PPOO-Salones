namespace Salones.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        // Relación uno a muchos con Reserva
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }

}
