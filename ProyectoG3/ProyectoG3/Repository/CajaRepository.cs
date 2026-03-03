using System.Data.SqlClient;
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

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT * FROM CAJAS WHERE IdComercio = @IdComercio", conn);

            cmd.Parameters.AddWithValue("@IdComercio", idComercio);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new Caja
                {
                    IdCaja = (int)reader["IdCaja"],
                    IdComercio = (int)reader["IdComercio"],
                    Nombre = reader["Nombre"].ToString()!,
                    Descripcion = reader["Descripcion"].ToString()!,
                    TelefonoSINPE = reader["TelefonoSINPE"].ToString()!,
                    FechaDeRegistro = (DateTime)reader["FechaDeRegistro"],
                    FechaDeModificacion = reader["FechaDeModificacion"] as DateTime?,
                    Estado = (bool)reader["Estado"]
                });
            }

            return lista;
        }

        public async Task<bool> ValidarNombreEnComercio(string nombre, int idComercio)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(
                "SELECT COUNT(*) FROM CAJAS WHERE Nombre = @Nombre AND IdComercio = @IdComercio",
                conn);

            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@IdComercio", idComercio);

            await conn.OpenAsync();
            int count = (int)await cmd.ExecuteScalarAsync();

            return count > 0;
        }

        public async Task<bool> ValidarTelefonoGlobal(string telefono)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(
                "SELECT COUNT(*) FROM CAJAS WHERE TelefonoSINPE = @Telefono",
                conn);

            cmd.Parameters.AddWithValue("@Telefono", telefono);

            await conn.OpenAsync();
            int count = (int)await cmd.ExecuteScalarAsync();

            return count > 0;
        }

        public async Task<Caja> ObtenerPorId(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT * FROM CAJAS WHERE IdCaja = @Id", conn);

            cmd.Parameters.AddWithValue("@Id", id);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Caja
                {
                    IdCaja = (int)reader["IdCaja"],
                    IdComercio = (int)reader["IdComercio"],
                    Nombre = reader["Nombre"].ToString()!,
                    Descripcion = reader["Descripcion"].ToString()!,
                    TelefonoSINPE = reader["TelefonoSINPE"].ToString()!,
                    FechaDeRegistro = (DateTime)reader["FechaDeRegistro"],
                    FechaDeModificacion = reader["FechaDeModificacion"] as DateTime?,
                    Estado = (bool)reader["Estado"]
                };
            }

            return null!;
        }

        public async Task<bool> Insertar(Caja caja)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(
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
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(
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