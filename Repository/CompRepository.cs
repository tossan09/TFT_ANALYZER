using Npgsql;
using TFTDataTrackerApi.Data;
using TFTDataTrackerApi.Models;

namespace TFTDataTrackerApi.Repository
{
    public class CompRepository
    {
        private readonly DbContext context;

        public CompRepository(DbContext context)
        {
            this.context = context;
        }

        public async Task<List<Comps>> ListarComps()
        {
            var comps = new List<Comps>();
            using var conexao = context.CriarConexao();
            await conexao.OpenAsync();

            using var comando = new NpgsqlCommand("SELECT * FROM comps", conexao);
            using var reader = await comando.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                comps.Add(new Comps
                {
                    id = reader.GetInt32(0),
                    name = reader.GetString(1),
                    traits = reader.GetString(2),
                    style = reader.GetString(3)
                });
            }
            return comps;
        }

        public async Task<List<Comps>> ListarCompsPorPatch(int setid)
        {
            var comps = new List<Comps>();
            using var conexao = context.CriarConexao();
            await conexao.OpenAsync();

            using var comando = new NpgsqlCommand("SELECT * FROM comps WHERE set_id = @setid", conexao);
            comando.Parameters.AddWithValue("@setid", setid);

            using var reader = await comando.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                comps.Add(new Comps
                {
                    id = reader.GetInt32(0),
                    name = reader.GetString(1),
                    traits = reader.GetString(2),
                    style = reader.GetString(3),
                    setid = reader.GetInt32(4)
                });
            }
            return comps;
        }


        public async Task AdicionarComp(Comps comps)
        {
            using var conexao = context.CriarConexao();
            await conexao.OpenAsync();

            var insertQuery = "INSERT INTO comps (name, traits, style, set_id) VALUES (@name, @traits, @style, @setid)";
            using var comando = new NpgsqlCommand(insertQuery, conexao);
            comando.Parameters.AddWithValue("@name", comps.name);
            comando.Parameters.AddWithValue("@traits", comps.traits);
            comando.Parameters.AddWithValue("@style", comps.style);
            comando.Parameters.AddWithValue("@setid", comps.setid);


            await comando.ExecuteNonQueryAsync();
        }
        
        public async Task<bool> EditComp(int id, Comps comps)
        {
            using var conexao = context.CriarConexao();
            await conexao.OpenAsync();

            var updateQuery = "UPDATE comps SET name = @name, traits = @traits, style = @style WHERE id = @id";
            using var comando = new NpgsqlCommand(updateQuery, conexao);
            comando.Parameters.AddWithValue("@name", comps.name);
            comando.Parameters.AddWithValue("@traits", comps.traits);
            comando.Parameters.AddWithValue("@style", comps.style);
            comando.Parameters.AddWithValue("@id", id);

            var linha = await comando.ExecuteNonQueryAsync();
            return linha > 0;
        }

        public async Task<bool> DeleteComp(int id)
        {
            using var conexao = context.CriarConexao();
            await conexao.OpenAsync();

            var deleteQuery = "DELETE FROM comps WHERE id = @id";
            using var comando = new NpgsqlCommand(deleteQuery, conexao);
            comando.Parameters.AddWithValue("id", id);

            var linha = await comando.ExecuteNonQueryAsync();
            return linha > 0;
        }

        //
        public async Task<List<Comps>> ListarCompsPorPatchId(int patchId)
        {
            using var conexao = context.CriarConexao();
            await conexao.OpenAsync();

            int setId = 0;
            using (var comando = new NpgsqlCommand("SELECT set_id FROM patches WHERE id = @patchId", conexao))
            {
                comando.Parameters.AddWithValue("@patchId", patchId);
                var result = await comando.ExecuteScalarAsync();
                if (result != null)
                {
                    setId = Convert.ToInt32(result);
                }
            }

            if (setId == 0) return new List<Comps>();

            return await ListarCompsPorPatch(setId);
        }

    }
}
