using School.DTOs;

namespace School.Models;
public record Teacher
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public long Contact { get; set; }
    public long SubjectId { get; set; }

    public TeacherDTO asDto => new TeacherDTO
    {
        Id = Id,
        FirstName = FirstName,
        LastName = LastName,
        Gender = Gender,
        DateOfBirth = DateOfBirth,
        Contact = Contact,
        SubjectId = SubjectId,
    };
 }