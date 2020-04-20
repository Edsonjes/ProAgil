
using Microsoft.AspNetCore.Mvc;
using ProAgil.Repository;
using Microsoft.AspNetCore.Http;
using ProAgil.Domain;
using System.Threading.Tasks;

namespace ProAgil.WebAPI.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]


    public class PalestranteController : ControllerBase
    {
      
      private readonly IProAgilRepository _repo;
        public PalestranteController( IProAgilRepository repo) 
        {
            _repo = repo;

        }


        [HttpGet("{PalestranteId}")]
        public async Task< IActionResult>   Get (int PalestranteId) 
        {
            try
            {
                var results = await _repo.GetAllPalestranteAsync(PalestranteId,true);

                 return Ok( results) ;
                
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

        }

        

       [HttpGet ("GetByNome/{Nome}")]
        public async Task< IActionResult>   Get (string Nome) 
        {
            try
            {
                var results = await _repo.GetAllPalestranteAsyncByName(Nome,true);

                 return Ok( results) ;
                
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

        }

        [HttpPost]
        public async Task <IActionResult> Post (Palestrante Model)
        {
            try
            {
                _repo.Add(Model);

                if (await _repo.SaveChangesAsync())
                {
                     return  Created($"/api/Palestrante/{Model.Id}",Model);


                }
                
            }
            catch (System.Exception)
            {
                
               return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
            return BadRequest();


        }

        [HttpPut]

        public async Task <IActionResult> Put( int PalestranteId, Palestrante Model)
        {

            try
            {
                 var Palestrante = _repo.GetAllPalestranteAsync(PalestranteId, false);
            if(Palestrante == null) return NotFound();

            _repo.Update(Model);

             if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/Palestrante/{Model.Id}",Model);

                }
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
            return BadRequest();
           
        }
        
        [HttpDelete]
        public async Task< IActionResult>   Delete ( int PalestranteId) 
        {
            try
            {
                var Palestrante = await _repo.GetAllPalestranteAsync (PalestranteId,false);
                if(Palestrante == null) return NotFound();

                _repo.Delete(Palestrante);

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