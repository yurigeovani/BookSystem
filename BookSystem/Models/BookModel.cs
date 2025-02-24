namespace BookSystem.Models {
    public class BookModel {
        public Guid Id { get; init; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; private set; }
        public AuthorModel Author { get; set; }

        public BookModel() {
            Id = Guid.NewGuid();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
