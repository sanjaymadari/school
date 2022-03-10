using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using School.Models;

namespace School.DTOs;

public record ClassesDTO
{
     [JsonPropertyName("class_id")]
     public long Id { get; set; }

     [JsonPropertyName("class")]
     public string Name { get; set; }

     [JsonPropertyName("students in this class")]
     public List<StudentDTO> Student { get; set;}
    
   


}

