using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using School.DTOs;

namespace School.Models;



public record Classes
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("class")]
    public string Name { get; set; }

    public ClassesDTO asDto => new ClassesDTO
    {
        Id = Id,
        Name = Name,
 
    };

}