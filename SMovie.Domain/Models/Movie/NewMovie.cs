using System.ComponentModel.DataAnnotations;

namespace SMovie.Domain.Models;

public class NewMovie
{
    public Guid MovieId { get; set; }
    public double Mark { get; set; }
    public int Time { get; set; }
    public int Viewer { get; set; }
    [MinLength(20)]
    public string Description { get; set; } = null!;
    [MaxLength(100)]
    [MinLength(2)]
    [Required]
    public string EnglishName { get; set; } = null!;
    [MaxLength(100)]
    [MinLength(2)]
    [Required]
    public string VietnamName { get; set; } = null!;
    public string Thumbnail { get; set; } = null!;
    [Required]
    public string Trailer { get; set; } = null!;
    [Required]
    public string NationId { get; set; } = null!;
    [Required]
    public int FeatureId { get; set; }
    [Required]
    public DateTime ProducedDate { get; set; }
    public IEnumerable<int> Categories { get; set; } = null!;
}
