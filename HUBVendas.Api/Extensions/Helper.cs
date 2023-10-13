using HUBVendas.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HUBVendas.Api.Extensions {
    public static class Helper {
        public static ActionResult InternalServerError<T>(this ControllerBase controllerBase, ResponseResult<T> response, Exception exception) {
            if (exception?.Message != null) {
                response.SetError(exception.Message);
            }
            else {
                response.SetError("Ocorreu um erro interno no servidor!");
            }


            return controllerBase.StatusCode(500, response);
        }
    }
}