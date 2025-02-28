namespace BookSystem.DTO {
    public class BookUpdateDTO {
        public string Title { get; set; } = "";
        public string Genre { get; set; } = "";
        public string Description { get; set; } = "";
        public Guid idAuthor { get; set; } = Guid.Empty;
    }
}
