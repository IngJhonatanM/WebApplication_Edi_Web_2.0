using EDIBANK.Conf_Db_With_Entity;
using EDIBANK.Models.Monitor;
using EDIBANK.Models.Users_EdiWeb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.CompilerServices;

namespace EDIBANK.Controllers;

public class MonitorController(UserManager<ApplicationUser> userManager, AppDbContext context) : Controller
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly AppDbContext _context = context;

    // GET: Intercambios/Monitor
    [Authorize]
    public async Task<IActionResult> Monitor(DateTime? desde, DateTime? hasta, bool? mostrarEntradas)
    {
        DateTime menor = (await _context.Intercambios.MinAsync(static DateTime (Intercambio i) => i.Cargado)).Date;
        DateTime mayor = DateTime.Today;
        ApplicationUser? user = await _userManager.GetUserAsync(HttpContext.User);

        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Usuario inválido (NO DEBE OCURRIR)");
            return View();
        }

        IList<string> roles = await _userManager.GetRolesAsync(user);

        desde ??= menor;
        hasta ??= mayor;
        if (hasta < desde)
        {
            ModelState.AddModelError(string.Empty, "Desde no puede ser mayor que Hasta");
        }
        hasta = hasta.Value.AddDays(1.0);
        return View(new MonitorViewModel
        {
            Intercambios = await (roles.Contains("Admin") ?
                                      from i in _context.Intercambios
                                      where desde <= i.Cargado && i.Cargado < hasta
                                      orderby i.Cargado descending
                                      select i :
                                  mostrarEntradas ?? true ?
                                      from i in _context.Intercambios
                                      where desde <= i.Cargado && i.Cargado < hasta && user.EDIId == i.ReceptorId
                                      orderby i.Cargado descending
                                      select i :
                                      from i in _context.Intercambios
                                      where desde <= i.Cargado && i.Cargado < hasta && user.EDIId == i.EmisorId
                                      orderby i.Cargado descending
                                      select i).ToListAsync(),
            Menor = menor,
            Desde = desde.Value,
            Mayor = mayor,
            Hasta = hasta.Value.AddDays(-1.0),
            MostrarEntradas = mostrarEntradas ?? true
        });
    }

    // POST: Intercambios/Recargar
    [HttpPost, ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Recargar(DateTime desde, DateTime hasta, bool mostrarEntradas, string origenId)
    {
        if (await _context.Intercambios.FindAsync(origenId) is Intercambio origen)
        {
            DateTime cargado = DateTime.Now;
            string id = $"{cargado:yyyyMMddHHmmssfff}";
            string archivoIntercambio = $"{origen.EmisorId}.{origen.ReceptorId}.{id}{Path.GetExtension(origen.ArchivoIntercambio)}";
            string path0 = Path.Join(origen.RutaArchivo, "RESPALDO", origen.ArchivoIntercambio);
            string path1 = Path.Join(origen.RutaArchivo, "ENTRADA", archivoIntercambio);
            string path2 = Path.Join(origen.RutaArchivo, "RESPALDO", archivoIntercambio);

            try
            {
                System.IO.File.Copy(path0, path1);
                await AuditarAsync<MonitorController>($"Entrada {path1} creada");
                try
                {
                    System.IO.File.Copy(path0, path2);
                    await AuditarAsync<MonitorController>($"Respaldo {path2} creado");
                    try
                    {
                        _context.Add(new Intercambio
                        {
                            Id = id,
                            Cargado = cargado,
                            Descargado = null,
                            Numero = origen.Numero,
                            TipoIntercambio = origen.TipoIntercambio,
                            EmisorId = origen.EmisorId,
                            ReceptorId = origen.ReceptorId,
                            TipoDocumento = origen.TipoDocumento,
                            ArchivoOriginal = origen.ArchivoOriginal,
                            ArchivoIntercambio = archivoIntercambio,
                            RutaArchivo = origen.RutaArchivo,
                            Tamano = origen.Tamano,
                            Status = Status.DISPONIBLE,
                            Emisor = null!,
                            Receptor = null
                        });
                        await AuditarAsync<MonitorController>($"{origenId} recargado como {id}");
                    }
                    catch
                    {
                        await AuditarAsync<MonitorController>($"{origenId} no recargado:");
                        System.IO.File.Delete(path2);
                        throw;
                    }
                }
                catch
                {
                    await AuditarAsync<MonitorController>($"Respaldo {path2} no creado:");
                    System.IO.File.Delete(path1);
                    throw;
                }
            }
            catch (Exception e)
            {
                await AuditarAsync<MonitorController>($"Entrada {path1} no creada:");
                await AuditarAsync<MonitorController>($"{e}");
            }
            finally
            {
                await _context.SaveChangesAsync();
            }
        }
        return RedirectToAction(nameof(Monitor), new { desde, hasta, mostrarEntradas });
        //return RedirectToAction(nameof(Monitor));
    }

    // POST: Intercambios/Remover
    [HttpPost, ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Remover(DateTime desde, DateTime hasta, bool mostrarEntradas, string origenId)
    {
        await Task.Delay(500);
        return RedirectToAction(nameof(Monitor), new { desde, hasta, mostrarEntradas });
        //return RedirectToAction(nameof(Monitor));
    }

    private async Task AuditarAsync<T>(string? comment, [CallerMemberName] string? memberName = null)
    {
        try
        {
            IPAddress? ip = HttpContext.Connection.RemoteIpAddress;

            _context.Add(new Auditoria
            {
                Fecha = DateTime.Now,
                Usuario = (await _userManager.GetUserAsync(HttpContext.User)).EDIId.Truncar(50) ?? "default",
                IPRemota = $"{(ip?.IsIPv4MappedToIPv6 is true ? ip.MapToIPv4() : ip)}",
                Modulo = typeof(T).FullName?.Truncar(50),
                Operacion = memberName?.Truncar(50),
                Comentario = comment?.Truncar(250)
            });
        }
        catch
        {
        }
    }
}
