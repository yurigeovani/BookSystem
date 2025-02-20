using System.Text.Json.Serialization;

namespace BookSystem.Models {
    public class AuthorModel {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public ICollection<BookModel> Books { get; set; }
    }
}
