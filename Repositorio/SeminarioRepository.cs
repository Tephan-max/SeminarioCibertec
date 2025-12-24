using Microsoft.Data.SqlClient;
using SeminarioCibertec.Models;

namespace SeminarioCibertec.Repositorio
{
    public class SeminarioRepository
    {
        private readonly string _cadenaConexion;
        public SeminarioRepository(string cadenaConexion) 
        { 
            _cadenaConexion = cadenaConexion;
        }
        public List<Seminario> listarSeminariosConCapacidad()
        {
            var seminarios = new List<Seminario>();

            using (var cnx = new SqlConnection(_cadenaConexion))
            using (var cmd = new SqlCommand("dbo.listaSeminariosConCapacidad", cnx))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cnx.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) {
                        var seminario = new Seminario
                        {
                            SeminarioId = Convert.ToInt32(reader["SeminarioId"]),
                            Curso = reader["SeminarioId"] as string,
                            Horario = reader["Horario"] as string,
                            Capacidad = Convert.ToInt32(reader["Capacidad"])
                        };
                        seminarios.Add(seminario);
                    }
                }
            }
            return seminarios;
        }

        public int registrarAsistencia(RegistroAsistencia ra)
        {
            int salida;

            using (var cnx = new SqlConnection(_cadenaConexion))
            using (var cmd = new SqlCommand("dbo.registrarAsistencia", cnx))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("seminarioId", ra.SeminarioId);
                cmd.Parameters.AddWithValue("estudianteId", ra.EstudianteId);
                cmd.Parameters.AddWithValue("fecha", ra.Fecha);
                cnx.Open();
                salida = cmd.ExecuteNonQuery();
            }
            return salida;
        }
    }
}
