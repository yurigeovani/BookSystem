using BookSystem.Models;

namespace BookSystem.DTO {
    public class BookResponseDTO () {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? Description { get; set; }
        public AuthorResponseDTO? Author { get; set; }
    }
}
