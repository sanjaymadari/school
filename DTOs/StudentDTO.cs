using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using School.Models;

namespace School.DTOs;

public record StudentAssignedDTO
{
    [JsonPropertyName("student_id")]
    public long Id { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
}

public record StudentDTO
{
    [JsonPropertyName("student_id")]
    public long Id { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }
    
    [JsonPropertyName("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("parent_contact")]
    public long ParentContact { get; set; }

    [JsonPropertyName("class_id")]
    public int ClassId { get; set; }


    [JsonPropertyName("teachers_assigned")]
    public List<TeacherDTO> Teacher {get; set; }

    [JsonPropertyName("subjects_enrolled")]
    public List<SubjectDTO> Subject { get; set; }
    

}

public record StudentCreateDTO
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
    
    [JsonPropertyName("parent_contact")]
    [Required]
    public long ParentContact { get; set; }

    [JsonPropertyName("class_id")]
    //[MaxLength(5)]
    public int ClassId { get; set; }
}

public record StudentUpdateDTO
{
    [JsonPropertyName("last_name")]
    [MaxLength(50)]
    public string LastName { get; set; }

    [JsonPropertyName("parent_contact")]
    public long? ParentContact { get; set; }
}