using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using School.Models;

namespace School.DTOs;

public record ClassesDTO
{
     [JsonPropertyName("id")]
     public long Id { get; set; }

     [JsonPropertyName("class")]
     public string Name { get; set; }

     [JsonPropertyName("students_in_class")]
     public List<StudentDTO> Student { get; set;}
}

public record ClassesListDTO
{
     [JsonPropertyName("id")]
     public long Id { get; set; }

     [JsonPropertyName("class")]
     public string Name { get; set; }

     [JsonPropertyName("students_in_class")]
     public int StudentCount { get; set;}
     
}