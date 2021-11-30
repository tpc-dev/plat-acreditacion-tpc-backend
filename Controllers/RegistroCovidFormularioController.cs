using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using PlatAcreditacionTPCBackend.Entidades;
using PlatAcreditacionTPCBackend.Models;
using PlatAcreditacionTPCBackend.Servicios;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/registro-covid-formulario")]
    public class RegistroCovidFormularioController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly IMailService mailService;
        private readonly IConfiguration configuration;

        private readonly string[] correosAdmins = new string []  { 
            "seba.manriquezh@gmail.com",
            "sebastian.manriquezh@gmail.com"
        };

        public RegistroCovidFormularioController(ApplicationDbContext context, IMailService mailService, IConfiguration configuration)
        {
            this.context = context;
            this.mailService = mailService;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<RegistroCovidFormulario>>> Get()
        {
            return await context.RegistrosCovidFormularios.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(RegistroCovidFormulario registroCovidFormulario)
        {
            registroCovidFormulario.Fecha = DateTime.Now;
            context.Entry(registroCovidFormulario).State = EntityState.Modified;
            context.Add(registroCovidFormulario);
            await context.SaveChangesAsync();
            if(registroCovidFormulario.HaTenidoSintomas || registroCovidFormulario.HaTenidoContactoEstrecho)
            {
                EnviarCorreoWarningProtocolo(registroCovidFormulario);
            }
            return Ok();
        }

        [HttpGet("ultimo-contestado/{rut}")]
        public async Task<ActionResult<RegistroCovidFormulario>> GetUltimoFormularioContestado(string rut)
        {
            bool existenRegistros = context.RegistrosCovidFormularios.Where(formulario => formulario.Rut == rut).AnyAsync().Result;

            if (!existenRegistros)
            {
                return Ok(new SimpleResponse() { Code = "002", Message = "No hay registros para este RUT" });
                // return BadRequest() ;
            }

            var fechaHoy = DateTime.Now;

            var registroCovidResult = await context.RegistrosCovidFormularios.OrderByDescending(formulario => formulario.Fecha).Where(formulario => formulario.Rut == rut).FirstAsync();

            if (registroCovidResult.Fecha.DayOfYear.Equals(fechaHoy.DayOfYear) &&
                registroCovidResult.Fecha.Month.Equals(fechaHoy.Month) &&
                registroCovidResult.Fecha.Year.Equals(fechaHoy.Year))
            {
                return registroCovidResult;
            }

            //return await context.RegistrosCovidFormularios.OrderByDescending(formulario=> formulario.Fecha).Where(formulario => formulario.Rut == rut).ToListAsync();
            //return await context.RegistrosCovidFormularios.OrderByDescending(formulario => formulario.Fecha).Where(formulario => formulario.Rut == rut).FirstAsync();
            return Ok(new SimpleResponse() { Code = "003", Message = "Hoy no ha contestado ningun formulario" }); ;
        }

        private async void EnviarCorreoWarningProtocolo(RegistroCovidFormulario registroCovidFormulario)
        {
            // CORREOS PARA AVISAR DE PELIGROS COVID

            var haTenidoSintomas = registroCovidFormulario.HaTenidoSintomas ? "SI" : "NO";
            var haTenidoContacto = registroCovidFormulario.HaTenidoContactoEstrecho ? "SI" : "NO";


            InternetAddressList list = new InternetAddressList();
            list.Add(new MailboxAddress(correosAdmins[0]));
            list.Add(new MailboxAddress(correosAdmins[1]));

            MailRequest mailRequest = new MailRequest
            {
                //ToEmail = nuevoUsuarioDTO.Email,
                //ToEmailList = "seba.manriquezh@gmail.com",
                ToEmailList = correosAdmins,
                Subject = "Emergencia Persona Formulario COVID-19 ingresos",
                Body = @"<html>
                                <h1>
                                    Una persona que hara ingreso a las intalaciones posee sintomas de COVID 19 o ha estado
                                    en contacto estrecho con alguien Positivo de COVID-19. 
                                </h1>
                                <p>
                                    Datos de la persona. <br>
                                </p>
                                <p>Nombre y Apellido: <strong>" + registroCovidFormulario.Nombre + " " + registroCovidFormulario.Apellido + @"</strong></p>
                                <p>¿Ha tenido Sintomas? <strong>" + haTenidoSintomas + @"</strong></p>
                                <p>¿Ha tenido contacto estrecho? <strong>" + haTenidoContacto + @"</strong></p>
                                <p>
                                    Saludos.
                                </p>
                            </html>"
            };

            try
            {
                await mailService.SendEmailAsync(mailRequest);
                //return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
