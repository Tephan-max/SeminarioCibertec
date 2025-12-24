namespace SeminarioCibertec.Models
{
    public class RegistroAsistencia
    {
        public int Registro { get; set; }
        public int SeminarioId { get; set; }
        public string EstudianteId { get; set; }
        public DateOnly ? Fecha { get; set; }
        public Seminario ? Seminario { get; set; }
    }
}
