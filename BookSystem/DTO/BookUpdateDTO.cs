namespace BookSystem.DTO {
    public record BookUpdateDTO (string Title, string Genre, string Description, Guid idAuthor);
}
