namespace SMovie.Domain.Entity
{
    public partial class Cast
    {
        public Guid ActorId { get; set; }
        public Guid MovieId { get; set; }
        public string CharacterName { get; set; } = null!;

        public virtual Person Actor { get; set; } = null!;
        public virtual Movie Movie { get; set; } = null!;
    }
}
