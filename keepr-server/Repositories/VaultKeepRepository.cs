using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr_server.Models;

namespace keepr_server.Repositories
{
    public class VaultKeepRepository
    {
        private readonly IDbConnection _db;
        public VaultKeepRepository(IDbConnection db)
        {
            _db = db;
        }
        public IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
        {
            string sql = @"
            SELECT k.*,
            vk.id as VaultKeepId,
            p.*
            FROM vaultkeeps vk
            JOIN keeps k ON k.id = vk.keepId
            JOIN profiles p ON p.id = k.creatorId
            WHERE vaultId = @VaultId;";
            return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { vaultId }, splitOn: "id");
        }
        public int Create(VaultKeep newVaultKeep)
        {
            string sql = @"
            INSERT INTO vaultkeeps
            (vaultId, keepId, creatorId)
            VALUES
            (@VaultId, @KeepId, @CreatorId);
            SELECT LAST_INSERT_ID();";
            return _db.ExecuteScalar<int>(sql, newVaultKeep);
        }
        public VaultKeep GetOne(int id)
        {
            string sql = @"
            SELECT * FROM vaultkeeps WHERE id = @Id;";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }
        public bool Delete(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @Id;";
            int affecttedRows = _db.Execute(sql, new { id });
            return affecttedRows > 0;
        }
    }
}