using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;
using ProAgil.WebAPI.Dtos;



namespace ProAgil.WebAPI.Controllers


{
     [Route("api/[Controller]")]
     
     [ApiController]

    public class EventoController : ControllerBase 
    {
       
        
            private readonly IProAgilRepository _repo;
            private readonly IMapper _mapper;

            public EventoController(IProAgilRepository repo, IMapper mapper)
            {
                _repo = repo;
                _mapper = mapper;
            }

        [HttpGet]
        
        public async Task<IActionResult> Get() 
        {
            try
            {
                var eventos = await _repo.GetAllEventoAsync(true);
                var results = _mapper.Map <EventosDtos[]>(eventos);

                 return  Ok( results) ;
                
            }
            catch (System.Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados Falhou {ex.Message}");
            }

        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resourcers", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, fileName.Replace("\"", " ").Trim());

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return Ok();
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco Dados Falhou {ex.Message}");
            }

            return BadRequest("Erro ao tentar realizar upload");
        }



        [HttpGet("{EventoId}")]
      
        public async Task<IActionResult> Get (int EventoId) 
        {
            try
            {
                var eventos = await _repo.GetEventoAsyncById (EventoId, true);
                var results = _mapper.Map <EventosDtos>(eventos);

                 return Ok( results) ;
                
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

        }

     [HttpGet ("GetByTema/{tema}")]
        public async Task< IActionResult>   Get ( string tema) 
        {
            try
            {
                var eventos = await _repo.GetAllEventoAsyncByTema(tema, true);
                var results = _mapper.Map <EventosDtos[]>(tema);

                 return Ok( results) ;
                
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

        }
        [HttpPost]
        public async Task< IActionResult> Post ( EventosDtos Model) 
        {
            try
            {
                var evento = _mapper.Map<Evento>(Model);
                _repo.Add(evento);

                if( await _repo.SaveChangesAsync())
                {
                    return  Created($"/api/evento/{Model.Id}",_mapper.Map<Evento>(evento));

                }

                
                
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
            return BadRequest();

        }

         
         [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put( int EventoId ,EventosDtos Model) 
        {
            try
            {
              



                var Evento = await _repo.GetEventoAsyncById (EventoId, false);
                if(Evento == null) return NotFound();

                  var idLotes = new List<int>();
                var idRedesSociais = new List<int>();

                Model.Lotes.ForEach(item => idLotes.Add(item.id));
                Model.RedesSociais.ForEach(item => idRedesSociais.Add(item.Id));

                var lotes = Evento.Lotes.Where(
                    lote => !idLotes.Contains(lote.id)
                ).ToArray();

                var redesSociais = Evento.RedesSociais.Where(
                    rede => !idLotes.Contains(rede.Id)
                ).ToArray();

                if (lotes.Length > 0) _repo.DeleteRange(lotes);
                if (redesSociais.Length > 0) _repo.DeleteRange(redesSociais);

               

               _mapper.Map(Model, Evento);
               
                _repo.Update(Evento);

                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{Model.Id}",_mapper.Map<Evento>(Evento));

                }
                return  BadRequest();

                
                
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
            return BadRequest();

        }

        [HttpDelete("{EventoId}")]
        public async Task< IActionResult>   Delete ( int EventoId) 
        {
            try
            {
                var Evento = await _repo.GetEventoAsyncById (EventoId, false);
                if(Evento == null) return NotFound();

                _repo.Delete(Evento);

                if( await _repo.SaveChangesAsync())
                {
                    return Ok();

                }

                
                
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
            return BadRequest();

        
    }   
        
    }  
}