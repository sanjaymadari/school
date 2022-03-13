using School.DTOs;
using School.Models;
using School.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace School.Controllers;

[ApiController]
[Route("api/subject")]
public class SubjectController : ControllerBase
{
    private readonly ILogger<SubjectController> _logger;
    private readonly ISubjectRepository _subject;
    private readonly ITeacherRepository _teacher;

    public SubjectController(ILogger<SubjectController> logger,
    ISubjectRepository subject, ITeacherRepository teacher)
    {
        _logger = logger;
        _subject = subject;
        _teacher = teacher;
    }

     [HttpGet]
     public async Task<ActionResult<List<SubjectDTO>>> GetAllClasses()
     {
         var subjectList = await _subject.GetList();

        // User -> UserDTO
         var dtoList = subjectList.Select(x => x.asDto);

         return Ok(dtoList);
     }
     [HttpGet("{id}")]
     public async Task<ActionResult<SubjectDTO>> GetSubjectById([FromRoute]long id)
     {
         var subject = await _subject.GetSubject(id);
         if (subject is null)
             return NotFound("No claas found with given Id");

        var dto = subject.asDto;

        dto.Teacher = await _teacher.GetTeacherForSubject(id);

         return Ok(dto);
     }

}
