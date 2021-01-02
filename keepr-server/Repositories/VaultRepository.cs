using Dapper;
using keepr_server.Models;
using System.Collections.Generic;
using System.Data;

namespace keepr_server.Repositories
{
    public class VaultRepository
    {
        private readonly IDbConnection _db;
        private readonly string populateCreator = "SELECT vault.*, profile.* FROM vaults vault INNER JOIN profiles profile ON vault.creatorId = profile.id";
        public VaultRepository(IDbConnection db)
        {
            _db = db;
        }
        public IEnumerable<Vault> GetByProfile(string creatorId)
        {
            string sql = @"
            SELECT
            vault.*,
            p.*
            FROM vaults vault
            JOIN profiles p ON vault.creatorId = p.id
            WHERE vault.creatorId = @creatorId;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, new { creatorId }, splitOn: "id");
        }
        public int Create(Vault newVault)
        {
            string sql = @"
            INSERT INTO vaults
            (name, description, creatorId, isPrivate)
            VALUES
            (@Name, @Description, @CreatorId, @IsPrivate);
            SELECT LAST_INSERT_ID();";
            return _db.ExecuteScalar<int>(sql, newVault);
        }
        internal Vault GetOne(int id)
        {
            string sql = "SELECT * FROM vaults WHERE id = @Id";
            return _db.QueryFirstOrDefault<Vault>(sql, new { id });
        }
        internal bool Delete(int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @Id";
            int affectedRows = _db.Execute(sql, new { id });
            return affectedRows > 0;
        }
    }
}