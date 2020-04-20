
using Microsoft.AspNetCore.Mvc;
using ProAgil.Repository;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ProAgil.Domain;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Web.Http;
using AutoMapper;
using System.Collections.Generic;
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
        public async Task< IActionResult> Get () 
        {
            try
            {
                var eventos = await _repo.GetAllEventoAsync(true);
                var results = _mapper.Map <EventosDtos[]>(eventos);

                 return Ok( results) ;
                
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

        }

         [HttpGet("{EventoId}")]
        public async Task< IActionResult> Get (int EventoId) 
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