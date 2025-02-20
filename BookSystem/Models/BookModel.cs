namespace BookSystem.Models {
    public class BookModel {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public AuthorModel Author { get; set; }
    }
}
