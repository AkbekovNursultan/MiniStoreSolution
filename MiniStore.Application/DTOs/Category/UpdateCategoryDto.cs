using System.ComponentModel.DataAnnotations;

namespace MiniStore.Application.DTOs.Category;

public record UpdateCategoryDto(
    [Required, MaxLength(100)] string Name
);
