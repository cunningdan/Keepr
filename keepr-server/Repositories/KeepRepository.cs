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
            string sql = @"
            SELECT 
            keep.*,
            p.*
             FROM keeps keep
             JOIN profiles p ON keep.creatorId = p.id;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, splitOn: "id");
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
        public Keep GetOne(int id)
        {
            string sql = @"
            SELECT * FROM keeps WHERE id = @Id;";
            return _db.QueryFirstOrDefault<Keep>(sql, new { id });
        }
        public bool Delete(int id)
        {
            string sql = "DELETE from keeps WHERE id = @Id";
            int valid = _db.Execute(sql, new { id });
            return valid > 0;
        }
        public void Edit(Keep keepData)
        {
            string sql = @"
            UPDATE keeps
            SET
            description = @Description,
            name = @Name,
            img = @Img,
            views = @Views,
            keeps = @Keeps,
            shares = @Shares
            WHERE id = @Id;";
            _db.Execute(sql, keepData);
        }
    }
}