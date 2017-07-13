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

        private const string stupid_select = "SELECT sql_no_cache substr(str, 1, 1) AS groupped FROM demo.strings s group by groupped";
        private const string big_select = "SELECT sql_no_cache * FROM demo.strings";

        public async Task<IEnumerable<string>> GetStringsAsyncConfigureAwait(bool continueOnCapturedContext)
        {
            using (var conn = GetConnection())
                return await conn.QueryAsync<string>(big_select).ConfigureAwait(continueOnCapturedContext);
        }

        public Task<IEnumerable<string>> GetStringsAsyncGottcha()
        {
            using (var conn = GetConnection())
                return conn.QueryAsync<string>(big_select);
        }

        public IEnumerable<string> GetStrings()
        {
            using (var conn = GetConnection())
                return conn.Query<string>(big_select);
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