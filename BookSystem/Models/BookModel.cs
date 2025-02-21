namespace BookSystem.Models {
    public class BookModel {
        public Guid Id { get; init; }
        public string Title { get; set; }
        public AuthorModel Author { get; set; }
        public DateTime CreatedAt { get; private set; }
    }
}
