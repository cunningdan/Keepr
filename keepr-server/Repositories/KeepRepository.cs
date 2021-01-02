using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr_server.Models;

namespace keepr_server.Repositories
{
    public class KeepRepository
    {
        private readonly IDbConnection _db;

        public KeepRepository(IDbConnection db)
        {
            _db = db;
        }
        public IEnumerable<Keep> GetAll()
        {
            string sql = "SELECT * FROM keeps";
            return _db.Query<Keep>(sql);
        }
        public IEnumerable<Keep> GetByProfile(string creatorId)
        {
            string sql = @"
            SELECT
            keep.*,
            p.*
            FROM keeps keep
            JOIN profiles p ON keep.creatorId = p.id
            WHERE keep.creatorId = @creatorId;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { creatorId }, splitOn: "id");
        }
        public int Create(Keep newKeep)
        {
            string sql = @"
            INSERT INTO keeps
            (name, description, img, creatorId)
            VALUES
            (@Name, @Description, @Img, @CreatorId);
            SELECT LAST_INSERT_ID();";
            return _db.ExecuteScalar<int>(sql, newKeep);
        }
    }
}