using System.Collections.Generic;
namespace ProAgil.WebAPI.Dtos
{
    public class PalestranteDtos
    {
        public int Id { get; set; }
        public string Nome{get; set;}
        public string MiniCurriculo { get; set; }
        public string ImagemURL  { get; set; }
        public string telefone { get; set; }
        public string Email { get; set; }
        public List<RedeSociaisDtos> RedesSociais { get; set; }
        public List<EventosDtos> Evento { get; set; }
    }
}