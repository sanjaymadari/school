using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using School.Models;

namespace School.DTOs;

public record TeacherDTO
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("contact")]
    public long Contact { get; set; }

    [JsonPropertyName("subject_id")]
    public long SubjectId { get; set; }

    [JsonPropertyName("subject_name")]
    public string SubjectName { get; set; }

    [JsonPropertyName("students_assigned")]
    public List<StudentDTO> Student { get; set; }

}

public record TeacherAssignDTO
{
    [JsonPropertyName("teacher_id")]
    public long Id { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("subject_id")]
    public long SubjectId { get; set; }
}

public record TeacherCreateDTO
{
    [JsonPropertyName("first_name")]
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    [MaxLength(50)]
    [Required]
    public string LastName { get; set; }

    [JsonPropertyName("gender")]
    [Required]
    [MaxLength(6)]
    public string Gender { get; set; }

    [JsonPropertyName("date_of_birth")]
    [Required]
    public DateTimeOffset DateOfBirth { get; set; } // 18 years of age

    [JsonPropertyName("contact")]
    [Required]
    public long Contact { get; set; }

    [JsonPropertyName("subject_id")]
    //[MaxLength(5)]
    public int SubjectId { get; set; }

}

public record TeacherUpdateDTO
{
    [JsonPropertyName("last_name")]
    [MaxLength(50)]
    public string LastName { get; set; }

    [JsonPropertyName("contact")]
    public long? Contact { get; set; }
}