using Npgsql;
using System.Text.RegularExpressions;
using TFTDataTrackerApi.Data;
using TFTDataTrackerApi.Models;

namespace TFTDataTrackerApi.Repository
{
    public class PatchRepository
    {
        private readonly DbContext _context;
        public PatchRepository(DbContext context)
        {
            _context = context;
        }


        public async Task<bool> AdicionarPatch(Patches patches)
        {
            using var conexao = _context.CriarConexao();
            await conexao.OpenAsync();

            var query = "INSERT INTO patches (patch_number, set_id) VALUES (@patchNumber, @setId)";
            using var comando = new NpgsqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@patchNumber", patches.patch_number);
            comando.Parameters.AddWithValue("@setId", patches.Set_id);

            var result = await comando.ExecuteNonQueryAsync();
            return result > 0;
        }

        public async Task<List<Patches>> ListarPatches()
        {
            var patches = new List<Patches>();
            using var conexao = _context.CriarConexao();
            await conexao.OpenAsync();

            var query = "SELECT id, patch_number, set_id FROM patches ORDER BY id DESC";
            using var comando = new NpgsqlCommand(query, conexao);

            using var reader = await comando.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                patches.Add(new Patches
                {
                    id = reader.GetInt32(0),
                    patch_number = reader.GetString(1),
                    Set_id = reader.GetInt32(2)
                });
            }
            return patches;
        }

        public async Task<List<Patches>> ListarPatchesPorSet(int setId)
        {
            var patches = new List<Patches>();
            using var conexao = _context.CriarConexao();
            await conexao.OpenAsync();

            using var comando = new NpgsqlCommand("SELECT id, patch_number, set_id FROM patches WHERE set_id = @setId ORDER BY id DESC", conexao);
            comando.Parameters.AddWithValue("@setId", setId);

            using var reader = await comando.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                patches.Add(new Patches
                {
                    id = reader.GetInt32(0),
                    patch_number = reader.GetString(1),
                    Set_id = reader.GetInt32(2)
                });
            }
            return patches;
        }

    }

}
