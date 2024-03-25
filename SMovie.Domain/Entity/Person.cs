namespace SMovie.Domain.Entity
{
    public partial class Person
    {
        public Person()
        {
            Casts = new HashSet<Cast>();
        }

        public Guid PersonId { get; set; }
        public string Thumbnail { get; set; } = null!;
        public string NamePerson { get; set; } = null!;
        public string NationId { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateTime? DoB { get; set; }
        public DateTime? DateCreated { get; set; }
        public virtual Nation Nation { get; set; } = null!;
        public virtual ICollection<Cast> Casts { get; set; } = null!;
    }
}
