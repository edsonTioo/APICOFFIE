namespace ApiMSCOFFIE.Models
{
    public class ActualizarReservacionDto
    {
        public DateTime Fecha { get; set; }             // Campo para la fecha de la reservación
        public string HoraReservacion { get; set; }      // Campo para la hora de inicio de la reservación
        public string HoraFinanReservacion { get; set; } // Campo para la hora de finalización de la reservación
    }
}
