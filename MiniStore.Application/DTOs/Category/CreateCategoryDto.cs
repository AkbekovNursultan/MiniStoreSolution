using System.ComponentModel.DataAnnotations;

namespace MiniStore.Application.DTOs.Category;

public record CreateCategoryDto(
    [Required, MaxLength(100)] string Name
);
