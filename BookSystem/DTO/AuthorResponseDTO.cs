namespace BookSystem.DTO {
    public record AuthorResponseDTO(Guid id, string firstName, string lastName, DateTime createdAt);
}
