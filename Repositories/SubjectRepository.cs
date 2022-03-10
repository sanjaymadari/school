using Dapper;
using School.DTOs;
using School.Models;
using School.Utilities;

namespace School.Repositories;

public interface ISubjectRepository
{
    Task<List<Subject>> GetList();
    Task<Subject> GetSubject(long Id);
    Task<List<SubjectDTO>> GetSubjects(long Id);
   // Task<List<SubjectsEnrolledDTO>> GetSubjects(long id);
}

public class SubjectRepository : BaseRepository, ISubjectRepository
 {
     public SubjectRepository(IConfiguration config) : base(config)
     {

     }

    public async Task<List<Subject>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.subject}"" ORDER BY id";

        List<Subject> res;
        using (var con = NewConnection) // Open connection
            res = (await con.QueryAsync<Subject>(query)).AsList(); // Execute the query
        // Close the connection
        return res;
    }

    public async Task<Subject> GetSubject(long Id)
    {
       var query = $@"SELECT * FROM ""{TableNames.subject}"" 
        WHERE id = @Id";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Subject>(query, new { Id });  
    }

    public async Task<List<SubjectDTO>> GetSubjects(long Id)
    {
         // var query = $@"SELECT * FROM {TableNames.teacher} WHERE id = (SELECT teacher_id FROM {TableNames.student_teacher} WHERE student_id = @StudentId)";
        var query = $@"SELECT * FROM {TableNames.student_subject} ss 
        LEFT JOIN {TableNames.subject} s ON s.id = ss.subject_id
         WHERE ss.student_id = @Id";

        using (var con = NewConnection)
        {
           // var ids =(await con.QueryAsync(query, new { Id })).AsList();
           // query = $@"SELECT * FROM {TableNames.teacher} WHERE id = {ids}";
            return (await con.QueryAsync<SubjectDTO>(query, new { Id })).AsList();
     }
    }
}