namespace BookSystem.DTO {
    public record BookCreateDTO (string Title, string Genre, string Description, Guid idAuthor);
}
