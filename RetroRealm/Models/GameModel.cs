using System.ComponentModel.DataAnnotations;

namespace RetroRealm.Models
{
    public class GameModel
    {
        public int GameModelId { get; set; }

        [Required (ErrorMessage = "Game title is required!")]
        public string? Title { get; set; }

        [Required (ErrorMessage = "A game code is required!")]
        public string? Code { get; set; }

        [Required (ErrorMessage = "A price is required!")]
        [Range (0f, float.MaxValue)]
        public decimal? Price { get; set; }

        [Required (ErrorMessage = "A release date is required!")]
        public DateOnly? ReleaseDate { get; set; }
    }
}
