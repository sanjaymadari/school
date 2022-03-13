using Dapper;
using School.DTOs;
using School.Models;
using School.Utilities;

namespace School.Repositories;

public interface IClassesRepository
{
    Task<List<Classes>> GetList();
    Task<Classes> GetClass(long id);
}

public class ClassesRepository : BaseRepository, IClassesRepository
 {
     public ClassesRepository(IConfiguration config) : base(config)
     {

     }

    public async Task<Classes> GetClass(long Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.class_room}"" 
        WHERE id = @Id";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Classes>(query, new { Id });       
    }

    public async Task<List<Classes>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.class_room}"" ORDER BY id";

        List<Classes> res;
        using (var con = NewConnection) // Open connection
            res = (await con.QueryAsync<Classes>(query)).AsList(); // Execute the query
        // Close the connection
        return res;
    }
}