using School.DTOs;
using School.Models;
using School.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace School.Controllers;

[ApiController]
[Route("api/class")]
public class ClassController : ControllerBase
{
    private readonly ILogger<ClassController> _logger;
    private readonly IClassesRepository _classes;
    private readonly IStudentRepository _student;

    public ClassController(ILogger<ClassController> logger,
    IClassesRepository classes, IStudentRepository student)
    {
        _logger = logger;
        _classes = classes;
        _student = student;
    }

     [HttpGet]  
     public async Task<ActionResult<List<ClassesDTO>>> GetAllClasses()
     {
         var classesList = await _classes.GetList();

        // User -> UserDTO
         var dtoList = classesList.Select(x => x.asListDto);

         return Ok(dtoList);
     }

     
     [HttpGet("{id}")]
     public async Task<ActionResult<ClassesDTO>> GetClassById([FromRoute]int id)
     {
         var singleClass = await _classes.GetClass(id);
         if (singleClass is null)
             return NotFound("No claas found with given Id");

        var dto = singleClass.asDto;

        dto.Student = await _student.GetStudentListInClass(id);

         return Ok(dto);
     }

}
