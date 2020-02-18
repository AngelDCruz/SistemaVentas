using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Peticiones
{
    public interface IHttpCliente
    {

        Task<string> ObtenerCadenaAsync(string uri, string autorizacionToken = null, string metodoAutorizacion = "Bearer");

        Task<HttpResponseMessage> PostAsync<T>(string uri,  T elemento, string autorizacionToken = null, string metodoAutorizacion = "Bearer");

        Task<HttpResponseMessage> DeleteAsync<T>(string uri, string autorizacionToken = null, string metodoAutorizacion = "Bearer");

        Task<HttpResponseMessage> PutAsync<T>(string uri, T elemento, string autorizacionToken = null, string metodoAutorizacion = "Bearer");

    }
}
