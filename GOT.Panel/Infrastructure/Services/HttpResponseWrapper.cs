using System.Security.Cryptography.X509Certificates;

namespace GOT.Panel.Infrastructure.Services
{
    public class HttpResponseWrapper<T>
    {
        public bool Error { get; set; }

        public T? Response { get; set; }

        public HttpResponseMessage HttpResponseMessage { get; set; }

        public string? ErrorMessage { get; set; }

        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage, string? errorMessage = null)
        {
            Error = error;
            Response = response;
            HttpResponseMessage = httpResponseMessage;
            ErrorMessage = errorMessage;        

        }

        public async Task<string> GetErrorMessage()
        {
            if (!Error) return string.Empty;

            var StatusCode = HttpResponseMessage.StatusCode;

            if(StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return "Recurso no encontrado";
            }

            if (StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return "Recurso no autorizado";
            }

            if (StatusCode == System.Net.HttpStatusCode.OK) 
            {
                return "Ok";
            }

            return "Ocurrio un error inesperado";
        }

    }
}
