using MySql.Data.MySqlClient;
using ProyectoG3.Models;

namespace ProyectoG3.Repository
{
    public class CajaRepository : ICajaRepository
    {
        private readonly string _connectionString;

        public CajaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Caja>> ObtenerCajasPorComercio(int idComercio)
        {
            var lista = new List<Caja>();

            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("SELECT * FROM CAJAS WHERE IdComercio = @IdComercio", conn);

            cmd.Parameters.AddWithValue("@IdComercio", idComercio);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new Caja
                {
                    IdCaja = Convert.ToInt32(reader["IdCaja"]),
                    IdComercio = Convert.ToInt32(reader["IdComercio"]),
                    Nombre = reader["Nombre"].ToString()!,
                    Descripcion = reader["Descripcion"].ToString()!,
                    TelefonoSINPE = reader["TelefonoSINPE"].ToString()!,
                    FechaDeRegistro = Convert.ToDateTime(reader["FechaDeRegistro"]),
                    FechaDeModificacion = reader["FechaDeModificacion"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["FechaDeModificacion"]),
                    Estado = Convert.ToBoolean(reader["Estado"])
                });
            }

            return lista;
        }

        public async Task<bool> ValidarNombreEnComercio(string nombre, int idComercio)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand(
                "SELECT COUNT(*) FROM CAJAS WHERE Nombre = @Nombre AND IdComercio = @IdComercio",
                conn);

            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@IdComercio", idComercio);

            await conn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result) > 0;
        }

        public async Task<bool> ValidarTelefonoGlobal(string telefono)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand(
                "SELECT COUNT(*) FROM CAJAS WHERE TelefonoSINPE = @Telefono",
                conn);

            cmd.Parameters.AddWithValue("@Telefono", telefono);

            await conn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result) > 0;
        }

        public async Task<Caja> ObtenerPorId(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("SELECT * FROM CAJAS WHERE IdCaja = @Id", conn);

            cmd.Parameters.AddWithValue("@Id", id);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Caja
                {
                    IdCaja = Convert.ToInt32(reader["IdCaja"]),
                    IdComercio = Convert.ToInt32(reader["IdComercio"]),
                    Nombre = reader["Nombre"].ToString()!,
                    Descripcion = reader["Descripcion"].ToString()!,
                    TelefonoSINPE = reader["TelefonoSINPE"].ToString()!,
                    FechaDeRegistro = Convert.ToDateTime(reader["FechaDeRegistro"]),
                    FechaDeModificacion = reader["FechaDeModificacion"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["FechaDeModificacion"]),
                    Estado = Convert.ToBoolean(reader["Estado"])
                };
            }

            return null!;
        }

        public async Task<bool> Insertar(Caja caja)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand(
                @"INSERT INTO CAJAS 
                (IdComercio, Nombre, Descripcion, TelefonoSINPE, FechaDeRegistro, Estado)
                VALUES 
                (@IdComercio, @Nombre, @Descripcion, @TelefonoSINPE, @FechaDeRegistro, @Estado)",
                conn);

            cmd.Parameters.AddWithValue("@IdComercio", caja.IdComercio);
            cmd.Parameters.AddWithValue("@Nombre", caja.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion", caja.Descripcion);
            cmd.Parameters.AddWithValue("@TelefonoSINPE", caja.TelefonoSINPE);
            cmd.Parameters.AddWithValue("@FechaDeRegistro", caja.FechaDeRegistro);
            cmd.Parameters.AddWithValue("@Estado", caja.Estado);

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Actualizar(Caja caja)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand(
                @"UPDATE CAJAS SET
                Nombre = @Nombre,
                Descripcion = @Descripcion,
                TelefonoSINPE = @TelefonoSINPE,
                FechaDeModificacion = @FechaDeModificacion,
                Estado = @Estado
                WHERE IdCaja = @IdCaja",
                conn);

            cmd.Parameters.AddWithValue("@Nombre", caja.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion", caja.Descripcion);
            cmd.Parameters.AddWithValue("@TelefonoSINPE", caja.TelefonoSINPE);
            cmd.Parameters.AddWithValue("@FechaDeModificacion", caja.FechaDeModificacion);
            cmd.Parameters.AddWithValue("@Estado", caja.Estado);
            cmd.Parameters.AddWithValue("@IdCaja", caja.IdCaja);

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}