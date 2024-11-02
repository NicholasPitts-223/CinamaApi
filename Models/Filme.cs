using CinemaApp.Models;

public class Filme
{
    public Filme() { } // Construtor padrão necessário para EF

    public Filme(int id, string genero, int cinemaId) 
    {
        Id = id;
        Genero = genero;
        CinemaId = cinemaId;
    }

    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Genero { get; set; }
    public int Duracao { get; set; } // em minutos
    public int CinemaId { get; set; }
    public Cinema Cinema { get; set; } // Navegação para o Cinema
}