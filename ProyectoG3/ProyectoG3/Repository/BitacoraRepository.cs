using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using ProyectoG3.Models;

namespace ProyectoG3.Repository
{
    public class BitacoraRepository : IBitacoraRepository
    {
        private readonly string _connectionString;

        public BitacoraRepository(IConfiguration configuration)
        {
            // Busca la cadena de conexion en appsettings.json
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Insertar(BitacoraEvento evento)   
        {
            using var connection = new SqlConnection(_connectionString);

            // Query basado estrictamente en los campos del enunciado
            string query = @"INSERT INTO BITACORA_EVENTOS 
                        (TablaDeEvento, TipoDeEvento, FechaDeEvento, DescripcionDeEvento, 
                         StackTrace, DatosAnteriores, DatosPosteriores) 
                        VALUES 
                        (@Tabla, @Tipo, @Fecha, @Desc, @Stack, @Ant, @Post)";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Tabla", evento.TablaDeEvento);
            command.Parameters.AddWithValue("@Tipo", evento.TipoDeEvento);
            command.Parameters.AddWithValue("@Fecha", evento.FechaDeEvento);
            command.Parameters.AddWithValue("@Desc", evento.DescripcionDeEvento);
            command.Parameters.AddWithValue("@Stack", evento.StackTrace ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Ant", (object)evento.DatosAnteriores ?? DBNull.Value);
            command.Parameters.AddWithValue("@Post", (object)evento.DatosPosteriores ?? DBNull.Value);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}

