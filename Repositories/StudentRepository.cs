using School.Models;
using Dapper;
using School.Utilities;
using School.DTOs;

namespace School.Repositories;
public interface IStudentRepository
{
     Task<Student> Create(Student Data);
     Task<List<Student>> GetList();
     Task<List<StudentDTO>> GetList(long TeacherId);
     Task<Student> GetById(long StudentId);
     Task Delete(long StudentId);
     Task<bool> Update(Student toUpdateStudent);
}
 public class StudentRepository : BaseRepository, IStudentRepository
 {
     public StudentRepository(IConfiguration config) : base(config)
     {

     }

    public async Task<Student> Create(Student Data)
    {
       var query = $@"INSERT INTO ""{TableNames.student}"" 
        (first_name, last_name, gender, date_of_birth, parent_contact, class_id) 
        VALUES (@FirstName, @LastName, @Gender, @DateOfBirth, @ParentContact, @ClassId) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Student>(query, Data);
            return res;
        }
    }

    public async Task Delete(long StudentId)
    {
        var query = $@"DELETE FROM ""{TableNames.student}"" 
        WHERE id = @StudentId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { StudentId });
            //return res > 0;
        }
    }

    public async Task<Student> GetById(long StudentId)
    {
        var query = $@"SELECT * FROM ""{TableNames.student}"" 
        WHERE id = @StudentId";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Student>(query, new { StudentId });
    }

    public async Task<List<Student>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.student}"" ORDER BY id";
        List<Student> res;
        using (var con = NewConnection) // Open connection
            res = (await con.QueryAsync<Student>(query)).AsList(); // Execute the query
        // Close the connection
        return res;
    }

    public async Task<List<StudentDTO>> GetList(long TeacherId)
    {
        var query = $@"SELECT * FROM {TableNames.student_teacher} st
        LEFT JOIN {TableNames.student} s ON s.id = st.student_id 
            WHERE st.teacher_id = @TeacherId";

        using (var con = NewConnection)
            return (await con.QueryAsync<StudentDTO>(query, new { TeacherId })).AsList();
    }

    public async Task<bool> Update(Student toUpdateStudent)
    {
         var query = $@"UPDATE ""{TableNames.student}"" SET address = @Address WHERE id = @Id";

            using (var con = NewConnection)
            {
                var rowCount = await con.ExecuteAsync(query, toUpdateStudent);
                return rowCount == 1;
            }
    }

}