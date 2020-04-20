using System.Collections.Generic;
namespace ProAgil.WebAPI.Dtos
{
    public class EventosDtos
    {
        public int Id { get; set; }
        public string Local { get; set; }
         public string DataEvento{get; set; }
        
        public string Tema {get; set;}
        public int  QtdPessoas {get;set;}
        
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }         
        public List<LotesDtos> Lotes { get; set; }
        public List<RedeSociaisDtos> RedesSociais { get; set; }
        public List<PalestranteDtos> Palestrante { get; set; }
    }
}