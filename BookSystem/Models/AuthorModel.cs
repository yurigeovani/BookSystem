using System.Text.Json.Serialization;
using BookSystem.DTO;

namespace BookSystem.Models {
    public class AuthorModel {
        public Guid Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; private set; }
        
        [JsonIgnore]
        public ICollection<BookModel>? Books { get; set; }

        //public AuthorModel() {
        //    Id = Guid.NewGuid();
        //    IsActive = true;
        //    CreatedAt = DateTime.UtcNow;
        //}
    }
}
