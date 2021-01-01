using CasaDoCodigo.Areas.Identity.Data;
using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CasaDoCodigo
{
  //TAREFA 05: INJETAR UserManager PARA OBTER clienteId
  public class HttpHelper : IHttpHelper
  {
    private readonly IHttpContextAccessor contextAccessor;
    public IConfiguration Configuration { get; }
    public readonly UserManager<AppIdentityUser> UserManager { get; }

    public HttpHelper(IHttpContextAccessor contextAccessor, IConfiguration configuration,
                      UserManager<AppIdentityUser> userManager)
    {
      this.contextAccessor = contextAccessor;
      Configuration = configuration;
      UserManager = userManager;
    }

    public int? GetPedidoId()
    {
      return contextAccessor.HttpContext.Session.GetInt32($"pedidoId_{GetClienteId()}");
    }

    private string GetClienteId()
    {
      var claimsPrincipal = contextAccessor.HttpContext.User;
      var clienteId = UserManager.GetUserId(claimsPrincipal);
      return clienteId;
    }

    public void SetPedidoId(int pedidoId)
    {
      contextAccessor.HttpContext.Session.SetInt32($"pedidoId_{GetClienteId()}", pedidoId);
    }

    public void ResetPedidoId()
    {
      contextAccessor.HttpContext.Session.Remove($"pedidoId_{GetClienteId()}");
    }
  }
}
