using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using School.DTOs;

namespace School.Models;



public record Subject
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
    public SubjectDTO asDto => new SubjectDTO
    {
        Id = Id,
        Name = Name,
    };

}