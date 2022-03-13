using School.DTOs;

namespace School.Models;
public record Student
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public long ParentContact { get; set; }
    public int ClassId { get; set; }


    public StudentDTO asDto => new StudentDTO
    {
        Id = Id,
        FirstName = FirstName,
        LastName = LastName,
        Gender = Gender,
        DateOfBirth = DateOfBirth.DateTime,
        ParentContact = ParentContact,
        ClassId =ClassId,
    };
}