using School.DTOs;
using School.Models;
using School.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace School.Controllers;

[ApiController]
[Route("api/teacher")]
public class TeacherController : ControllerBase
{
    private readonly ILogger<TeacherController> _logger;
    private readonly ITeacherRepository _teacher;
    private readonly IStudentRepository _student;
   

    public TeacherController(ILogger<TeacherController> logger,
    ITeacherRepository teacher, IStudentRepository student)
    {
        _logger = logger;
        _teacher = teacher;
        _student =student;
       //  _class = presentClass;
    }

     [HttpGet]
     public async Task<ActionResult<List<TeacherDTO>>> GetAllTeachers()
     {
        var teachersList = await _teacher.GetList();

        var dtoList = teachersList.Select(x => x.asDto);

         return Ok(dtoList);
     }

     [HttpGet("{teacher_id}")]
     public async Task<ActionResult<TeacherDTO>> GetTeacherById([FromRoute] long teacher_id)
     {
         var teacher = await _teacher.GetById(teacher_id);

         if (teacher is null)
             return NotFound("No teacher found with given teacher id");

         var dto = teacher.asDto;

         dto.Student = await _student.GetList(teacher.Id);

         return Ok(dto);
     }

     [HttpPost]
     public async Task<ActionResult<TeacherDTO>> CreateTeacher([FromBody] TeacherCreateDTO Data)
     {
        if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
            return BadRequest("Gender value is not recognized");

        var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        if (subtractDate.TotalDays / 365 < 18.0)
            return BadRequest("Teacher must be at least 18 years old");

        var toCreateTeacher = new Teacher
        {
            FirstName = Data.FirstName.Trim(),
            LastName = Data.LastName.Trim(),
            Gender = Data.Gender,//Enum.Parse<Gender>(Data.Gender, true),
            DateOfBirth = Data.DateOfBirth.UtcDateTime,
            Contact = Data.Contact,
            SubjectId = Data.SubjectId,
        };

        var createdTeacher = await _teacher.Create(toCreateTeacher);

         return StatusCode(StatusCodes.Status201Created, createdTeacher.asDto);
     }

     [HttpPut("{teacher_id}")]
     public async Task<ActionResult> UpdateTeacher([FromRoute] long teacher_id,
     [FromBody] TeacherUpdateDTO Data)
     {
        var existing = await _teacher.GetById(teacher_id);
        if (existing is null)
            return NotFound("No Teacher found with given teacher id");

        var toUpdateTeacher = existing with
        {
           // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
           LastName = Data.LastName?.Trim() ?? existing.LastName,
           Contact = Data.Contact ?? existing.Contact,
           // DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _teacher.Update(toUpdateTeacher);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update user");

         return NoContent();
     }

     [HttpDelete("{teacher_id}")]
     public async Task<ActionResult> DeleteStudent([FromRoute] long teacher_id)
     {
        var existing = await _teacher.GetById(teacher_id);
        if (existing is null)
            return NotFound("No Teacher found with given Id");

        var didDelete = await _teacher.Delete(teacher_id);

         return NoContent();
     }
}
