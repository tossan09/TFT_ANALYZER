using Npgsql;
using TFTDataTrackerApi.Data;
using TFTDataTrackerApi.Models;

namespace TFTDataTrackerApi.Repository
{
    public class SetRepository(DbContext context)
    {
        private readonly DbContext _context = context;

        public async Task<bool> AdicionarSet(Sets sets)
        {
            using var conexao = _context.CriarConexao();
            await conexao.OpenAsync();

            var query = "INSERT INTO sets (set_number) VALUES (@setNumber)";
            using var comando = new NpgsqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@setNumber", sets.SetNumber);

            var result = await comando.ExecuteNonQueryAsync();
            return result > 0;
        }

        public async Task<List<Sets>> ListarSets()
        {
            var sets = new List<Sets>();
            using var conexao = _context.CriarConexao();
            await conexao.OpenAsync();

            var query = "SELECT id, set_number FROM sets ORDER BY set_number DESC";
            using var comando = new NpgsqlCommand(query, conexao);

            using var reader = await comando.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                sets.Add(new Sets
                {
                    Id = reader.GetInt32(0),
                    SetNumber = reader.GetInt32(1)
                });
            }
            return sets;
        }
    }
}