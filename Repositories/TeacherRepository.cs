using School.Models;
using Dapper;
using School.Utilities;
using School.DTOs;

namespace School.Repositories;

public interface ITeacherRepository
{
    Task<Teacher> Create(Teacher Item);
    Task<bool> Update(Teacher Item);
    Task<bool> Delete(long TeacherId);
    Task<Teacher> GetById(long TeacherId);
    Task<List<Teacher>> GetList();
    Task<List<TeacherDTO>> GetList(long Id);

}
public class TeacherRepository : BaseRepository, ITeacherRepository
{
    public TeacherRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Teacher> Create(Teacher Data)
    {
        var query = $@"INSERT INTO ""{TableNames.teacher}"" 
        (first_name, last_name, gender, date_of_birth, contact, subject_id) 
        VALUES (@FirstName, @LastName, @Gender, @DateOfBirth, @Contact, @SubjectId) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Teacher>(query, Data);
            return res;
        }
    }

    public async Task<bool> Delete(long TeacherId)
    {
        var query = $@"DELETE FROM ""{TableNames.teacher}"" 
        WHERE id = @TeacherId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { TeacherId });
            return res > 0;
        }

    }

    public async Task<Teacher> GetById(long Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.teacher}"" 
        WHERE id = @Id";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Teacher>(query, new { Id });
    }

    public async Task<List<Teacher>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.teacher}"" ORDER BY id";
        List<Teacher> res;
        using (var con = NewConnection) // Open connection
            res = (await con.QueryAsync<Teacher>(query)).AsList(); // Execute the query
        // Close the connection
        return res;
    }

    public async Task<List<TeacherDTO>> GetList(long Id)
    {
        // var query = $@"SELECT * FROM {TableNames.teacher} WHERE id = (SELECT teacher_id FROM {TableNames.student_teacher} WHERE student_id = @StudentId)";
        var query = $@"SELECT t.*, s.name AS subject_name FROM {TableNames.student_teacher} st 
        LEFT JOIN {TableNames.teacher} t ON t.id = st.teacher_id
        LEFT JOIN {TableNames.subject} s ON s.id = t.subject_id
        WHERE st.student_id = @Id";

        using (var con = NewConnection)
        {
            // var ids =(await con.QueryAsync(query, new { Id })).AsList();
            // query = $@"SELECT * FROM {TableNames.teacher} WHERE id = {ids}";
            return (await con.QueryAsync<TeacherDTO>(query, new { Id })).AsList();
        }
    }

    public async Task<bool> Update(Teacher Item)
    {
        var query = $@"UPDATE ""{TableNames.teacher}"" SET contact = @Contact, last_name = @LastName WHERE id = @Id";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, Item);
            return rowCount == 1;
        }
    }

}