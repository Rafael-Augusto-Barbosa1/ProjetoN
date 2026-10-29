using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoNilson4.Libraries.Login;
using ProjetoNilson4.Models;
using ProjetoNilson4.Models.Constant;

namespace ProjetoNilson4.Libraries.Filtro
{
    public class ColaboradorAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        private string _tipoColaboradorAutorizado;
        public ColaboradorAutorizacaoAttribute(string tipoColaboradorAutorizado = ColaboradorTipoConstant.Comum)
        {
            _tipoColaboradorAutorizado = tipoColaboradorAutorizado;
        }

        LoginColaborador _loginColaborador;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginColaborador = (LoginColaborador)context.HttpContext.RequestServices.GetService(typeof(LoginColaborador));
            Colaborador colaborador = _loginColaborador.GetColaborador();
            if (colaborador == null)
            {
                context.Result = new RedirectToActionResult("LoginColaborador", "Home", null);
            }
            else
            {
                if (colaborador.Tipo == ColaboradorTipoConstant.Comum && _tipoColaboradorAutorizado == ColaboradorTipoConstant.Gerente)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
