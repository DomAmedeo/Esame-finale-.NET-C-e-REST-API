namespace Its_31_Esame_RestApi_Sablone.Models
{
    public class Autore
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public List<Libro> Libri { get; set; } = new List<Libro>();
    }
}
