namespace BookSystem.DTO {
    public class AuthorResponseDTO {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
