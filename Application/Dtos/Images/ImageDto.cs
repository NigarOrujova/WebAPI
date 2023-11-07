using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Images;

public class ImageDto
{
    public string? ImagePath { get; set; }
    public IFormFile Image { get; set; }
    public string? ImageAlt { get; set; }
    public string? ImageAltAz { get; set; }
    public bool IsMain { get; set; }
    public int? PortfolioId { get; set; }
    public Portfolio? Portfolio { get; set; }
}
