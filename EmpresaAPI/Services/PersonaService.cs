using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;
using Services;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Data
{
    public class PersonaService : IPersonaService
    {
        private readonly string _connectionString;

        public PersonaService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Métodos para Personas Naturales

        public async Task<int> CreatePersonaNaturalAsync(PersonaNatural persona)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("dbo.sp_PersonaNatural_Create", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@TipoDocumento", persona.TipoDocumento);
            command.Parameters.AddWithValue("@NumeroDocumento", persona.NumeroDocumento);
            command.Parameters.AddWithValue("@Nombres", persona.Nombres);
            command.Parameters.AddWithValue("@ApellidoPaterno", persona.ApellidoPaterno);
            command.Parameters.AddWithValue("@ApellidoMaterno", persona.ApellidoMaterno);
            command.Parameters.AddWithValue("@Edad", persona.Edad);
            command.Parameters.AddWithValue("@Sexo", persona.Sexo);
            command.Parameters.AddWithValue("@Email", persona.Email ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimiento ?? (object)DBNull.Value);

            await connection.OpenAsync();
            return (int)await command.ExecuteScalarAsync();
        }

        public async Task<int> UpdatePersonaNaturalAsync(int id, PersonaNatural persona)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("dbo.sp_PersonaNatural_Update", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@PersonaId", id);
            command.Parameters.AddWithValue("@TipoDocumento", persona.TipoDocumento);
            command.Parameters.AddWithValue("@NumeroDocumento", persona.NumeroDocumento);
            command.Parameters.AddWithValue("@Nombres", persona.Nombres);
            command.Parameters.AddWithValue("@ApellidoPaterno", persona.ApellidoPaterno);
            command.Parameters.AddWithValue("@ApellidoMaterno", persona.ApellidoMaterno);
            command.Parameters.AddWithValue("@Edad", persona.Edad);
            command.Parameters.AddWithValue("@Sexo", persona.Sexo);
            command.Parameters.AddWithValue("@Email", persona.Email ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimiento ?? (object)DBNull.Value);

            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> DeletePersonaNaturalAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("dbo.sp_PersonaNatural_Delete", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@PersonaId", id);

            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<PersonaNatural> GetPersonaNaturalByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("dbo.sp_PersonaNatural_GetById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@PersonaId", id);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new PersonaNatural
                {
                    PersonaId = reader.GetInt32(0),
                    TipoDocumento = reader.GetString(1),
                    NumeroDocumento = reader.GetString(2),
                    Nombres = reader.GetString(3),
                    ApellidoPaterno = reader.GetString(4),
                    ApellidoMaterno = reader.GetString(5),
                    Edad = reader.GetByte(6),
                    Sexo = reader.GetString(7),
                    Email = reader.IsDBNull(8) ? null : reader.GetString(8),
                    FechaNacimiento = reader.IsDBNull(9) ? null : reader.GetDateTime(9),
                    FechaRegistro = reader.GetDateTime(10)
                };
            }
            return null;
        }

        public async Task<IEnumerable<PersonaNatural>> ListPersonaNaturalesAsync(string filtro)
        {
            var personaList = new List<PersonaNatural>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("dbo.sp_PersonaNatural_List", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Filtro", filtro ?? (object)DBNull.Value);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                personaList.Add(new PersonaNatural
                {
                    PersonaId = reader.GetInt32(0),
                    TipoDocumento = reader.GetString(1),
                    NumeroDocumento = reader.GetString(2),
                    Nombres = reader.GetString(3),
                    ApellidoPaterno = reader.GetString(4),
                    ApellidoMaterno = reader.GetString(5),
                    Edad = reader.GetByte(6),
                    Sexo = reader.GetString(7),
                    Email = reader.IsDBNull(8) ? null : reader.GetString(8),
                    FechaNacimiento = reader.IsDBNull(9) ? null : reader.GetDateTime(9),
                    FechaRegistro = reader.GetDateTime(10)
                });
            }

            return personaList;
        }

        // Métodos para Personas Jurídicas

        public async Task<int> CreatePersonaJuridicaAsync(PersonaJuridica persona)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("dbo.sp_PersonaJuridica_Create", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@RazonSocial", persona.RazonSocial);
            command.Parameters.AddWithValue("@TipoDocumento", persona.TipoDocumento);
            command.Parameters.AddWithValue("@NumeroDocumento", persona.NumeroDocumento);

            await connection.OpenAsync();
            return (int)await command.ExecuteScalarAsync();
        }

        public async Task<int> UpdatePersonaJuridicaAsync(int id, PersonaJuridica persona)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("dbo.sp_PersonaJuridica_Update", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@PersonaJuridicaId", id);
            command.Parameters.AddWithValue("@RazonSocial", persona.RazonSocial);
            command.Parameters.AddWithValue("@TipoDocumento", persona.TipoDocumento);
            command.Parameters.AddWithValue("@NumeroDocumento", persona.NumeroDocumento);

            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> DeletePersonaJuridicaAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("dbo.sp_PersonaJuridica_Delete", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@PersonaJuridicaId", id);

            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<PersonaJuridica> GetPersonaJuridicaByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("dbo.sp_PersonaJuridica_GetById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@PersonaJuridicaId", id);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new PersonaJuridica
                {
                    PersonaJuridicaId = reader.GetInt32(0),
                    RazonSocial = reader.GetString(1),
                    TipoDocumento = reader.GetString(2),
                    NumeroDocumento = reader.GetString(3),
                    FechaRegistro = reader.GetDateTime(4)
                };
            }
            return null;
        }

        public async Task<IEnumerable<PersonaJuridica>> ListPersonaJuridicasAsync(string filtro)
        {
            var personaJuridicaList = new List<PersonaJuridica>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("dbo.sp_PersonaJuridica_List", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Filtro", filtro ?? (object)DBNull.Value);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                personaJuridicaList.Add(new PersonaJuridica
                {
                    PersonaJuridicaId = reader.GetInt32(0),
                    RazonSocial = reader.GetString(1),
                    TipoDocumento = reader.GetString(2),
                    NumeroDocumento = reader.GetString(3)
                });
            }

            return personaJuridicaList;
        }
    }
}
