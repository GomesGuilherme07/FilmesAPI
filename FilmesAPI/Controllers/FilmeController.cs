using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        /*private static List<Filme> filmes = new List<Filme>();
        private static int id = 1;*/

        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost] //Criar um recurso
        public IActionResult AdcionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            /*filme.Id = id++;
            filmes.Add(filme);*/
            //Console.WriteLine(filme.Titulo);

            Filme filme = _mapper.Map<Filme>(filmeDto);


            _context.Filmes.Add(filme);
            _context.SaveChanges();//Salvar alterações no banco
            return CreatedAtAction(nameof(RetornaFilmePorID), new { Id = filme.Id }, filme);
        }

        [HttpGet] //Recuperar um recurso
        public IEnumerable<Filme> RetornaFilmes()
        {
            //return Ok(filmes);
            return(_context.Filmes);
        }

        [HttpGet("{id}")]
        public IActionResult RetornaFilmePorID(int id)
        {
            //Filme filme = filmes.FirstOrDefault(filme => filme.Id == id);
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);

                return Ok(filmeDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if(filme == null)
            {
                return NotFound();
            }
            _mapper.Map(filmeDto, filme); //sobrescrevendo as informãções de filmeDto e jogando em filme
            _context.SaveChanges();
            return NoContent();
            
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound();
            }
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }


    }
}
