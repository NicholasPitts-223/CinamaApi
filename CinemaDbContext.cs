namespace CinemaApp.Models
{
    public class Filme
    {
        // Construtor padrão necessário para EF
        public Filme() { }

        // Construtor com parâmetros
        public Filme(int id, string titulo, string genero, int duracao, int cinemaId) 
        {
            Id = id;
            Titulo = titulo; // Adicionei Titulo aqui
            Genero = genero;
            Duracao = duracao; // Adicionei Duracao aqui
            CinemaId = cinemaId;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public int Duracao { get; set; } // em minutos
        public int CinemaId { get; set; }

        // Navegação para o Cinema
        public Cinema Cinema { get; set; }
    }
}