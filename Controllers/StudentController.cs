using School.DTOs;
using School.Models;
using School.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace School.Controllers;

[ApiController]
[Route("api/student")]
public class StudentController : ControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly IStudentRepository _student;
    private readonly ITeacherRepository _teacher;
    private readonly ISubjectRepository _stubject;
    

    public StudentController(ILogger<StudentController> logger,
    IStudentRepository student, ITeacherRepository teacher, ISubjectRepository subject)
    {
        _logger = logger;
        _student = student;
        _teacher= teacher;
        _stubject = subject;
    }


    //Get Student List
     [HttpGet]
     public async Task<ActionResult<List<StudentDTO>>> GetAllStudents()
     {
         var studentsList = await _student.GetList();
         var dtoList = studentsList.Select(x => x.asDto);
         return Ok(dtoList);
     }


    //Get Single Student Data
     [HttpGet("{student_id}")]
     public async Task<ActionResult<StudentDTO>> GetStudentById([FromRoute] long student_id)
     {
         var student = await _student.GetById(student_id);

         if (student is null)
             return NotFound("No student found with given Id");

        var dto = student.asDto;

        dto.Teacher = await _teacher.GetList(student.Id);
        dto.Subject = await _stubject.GetSubjects(student.Id);

         return Ok(dto);
     }




     //Create Student
     [HttpPost]
     public async Task<ActionResult<StudentDTO>> CreateStudent([FromBody] StudentCreateDTO Data)
     {
        if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
              return BadRequest("Gender value is not recognized");

        var toCreateStudent = new Student
        {
            FirstName = Data.FirstName,
            LastName = Data.LastName,
            Gender = Data.Gender,
            DateOfBirth = Data.DateOfBirth.UtcDateTime,
            ParentContact = Data.ParentContact,
            ClassId = Data.ClassId,
        };

         var createdStudent = await _student.Create(toCreateStudent);

         return StatusCode(StatusCodes.Status201Created, createdStudent.asDto);
        
     }



     //Update Student Details
     [HttpPut("{student_id}")]
     public async Task<ActionResult> UpdateStudent([FromRoute] long student_id,
     [FromBody] StudentUpdateDTO Data)
     {
        var existing = await _student.GetById(student_id);
        if (existing is null)
            return NotFound("No user found with given employee number");

         var toUpdateStudent = existing with
        {
            LastName = Data.LastName?.Trim() ?? existing.LastName,
            ParentContact = Data.ParentContact ?? existing.ParentContact,
        };

        var didUpdate = await _student.Update(toUpdateStudent);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update user");

         return NoContent();
     }



     //Delete Single Student
     [HttpDelete("{student_id}")]
     public async Task<ActionResult> DeleteStudent([FromRoute] long student_id)
     {
        var existing = await _student.GetById(student_id);
        if (existing is null)
            return NotFound("No user found with given employee number");

         await _student.Delete(student_id);

         return NoContent();
     }
}
