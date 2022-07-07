using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Data;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly DataContext _context;

        public EventoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _context.Eventos;
            // return new Evento[] {
            //     new Evento() {
            //     EventoId = 1,
            //     Tema = "Angular",
            //     Local = "Belo Jardim",
            //     QtdPessoas = 150
            //     }
            // };
        }
    }
}