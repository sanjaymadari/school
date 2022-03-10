
using System.Text.Json.Serialization;
using School.Models;

namespace School.DTOs;
public record SubjectsEnrolledDTO
{
    public long Id { get; set; }
    public string Name { get; set; }
}
public record SubjectDTO
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    [JsonPropertyName("who theaches")]
    public List<TeacherDTO> Teacher { get; set; }
}