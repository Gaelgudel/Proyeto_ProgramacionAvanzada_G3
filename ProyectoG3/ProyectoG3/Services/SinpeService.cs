using Microsoft.Data.SqlClient;
using ProyectoG3.Models;

public async Task<IEnumerable<Sinpe>> ObtenerSinpePorTelefono(string telefono)
{
    var lista = new List<Sinpe>();

    using var conn = new SqlConnection(_connectionString);
    using var cmd = new SqlCommand(
        @"SELECT * FROM SINPES 
          WHERE TelefonoDestino = @Telefono
          ORDER BY Fecha DESC", conn);

    cmd.Parameters.AddWithValue("@Telefono", telefono);

    await conn.OpenAsync();
    using var reader = await cmd.ExecuteReaderAsync();

    while (await reader.ReadAsync())
    {
        lista.Add(new Sinpe
        {
            IdSinpe = (int)reader["IdSinpe"],
            TelefonoOrigen = reader["TelefonoOrigen"].ToString()!,
            NombreOrigen = reader["NombreOrigen"].ToString()!,
            TelefonoDestino = reader["TelefonoDestino"].ToString()!,
            NombreDestino = reader["NombreDestino"].ToString()!,
            Monto = (decimal)reader["Monto"],
            Descripcion = reader["Descripcion"].ToString()!,
            Fecha = (DateTime)reader["Fecha"],
            Estado = (int)reader["Estado"]
        });
    }

    return lista;
}