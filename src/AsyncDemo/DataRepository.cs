namespace AsyncDemo
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using MySql.Data.MySqlClient;

    public class DataRepository
    {
        private readonly string _connectionString;

        public DataRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<string>> GetStringsAsyncConfigureAwait(bool continueOnCapturedContext)
        {
            using (var conn = GetConnection())
                return await conn.QueryAsync<string>("SELECT str FROM strings").ConfigureAwait(continueOnCapturedContext);
        }

        public Task<IEnumerable<string>> GetStringsAsyncGottcha()
        {
            using (var conn = GetConnection())
                return conn.QueryAsync<string>("SELECT str FROM strings");
        }

        public IEnumerable<string> GetStrings()
        {
            using (var conn = GetConnection())
                return conn.Query<string>("SELECT str FROM strings");
        }

        public void Seed(List<string> strings)
        {
            using (var conn = GetConnection())
            {
                foreach (var str in strings)
                {
                    conn.Execute("INSERT INTO strings (str) VALUES (@str)", new {str});
                }
            }
        }

        public async Task SeedAsync(List<string> strings)
        {
            using (var conn = GetConnection())
            {
                foreach (var str in strings)
                {
                    await conn.ExecuteAsync("INSERT INTO strings (str) VALUES (@str)", new {str}).ConfigureAwait(false);
                }
            }
        }

        public Task SeedAsync2(List<string> strings)
        {
            return Task.WhenAll(strings.Select(Insert));
        }

        private async Task Insert(string str)
        {
            using (var conn = GetConnection())
            {
                await conn.ExecuteAsync("INSERT INTO strings (str) VALUES (@str)", new { str }).ConfigureAwait(false);
            }
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}