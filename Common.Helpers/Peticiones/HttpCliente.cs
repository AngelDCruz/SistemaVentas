using Common.Excepciones;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Common.Peticiones
{
    public class HttpCliente : IHttpCliente
    {
        private HttpClient _httpClient;
        private readonly HttpContextAccessor _httpContextAccessor;

        public HttpCliente()
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = new HttpContextAccessor();
        }


        public async Task<string> ObtenerCadenaAsync(string uri, string autorizacionToken = null, string metodoAutorizacion = "Bearer")
        {

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);

            if(autorizacionToken != null)
            {

                request.Headers.Authorization = new AuthenticationHeaderValue(metodoAutorizacion, autorizacionToken);

            }

            var respuesta = await _httpClient.SendAsync(request);

            return await respuesta.Content.ReadAsStringAsync();

        }

        public async Task<HttpResponseMessage> DeleteAsync<T>(string uri, string autorizacionToken = null, string metodoAutorizacion = "Bearer")
        {

            var request = new HttpRequestMessage(HttpMethod.Delete, uri);

            if(autorizacionToken != null)
            {

                request.Headers.Authorization = new AuthenticationHeaderValue(metodoAutorizacion, autorizacionToken);

            }

            return await _httpClient.SendAsync(request);

        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T elemento, string autorizacionToken = null, string metodoAutorizacion = "Bearer")
        {
            return await PeticionPostPutAsync(HttpMethod.Post, uri, elemento, autorizacionToken, metodoAutorizacion);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T elemento, string autorizacionToken = null, string metodoAutorizacion = "Bearer")
        {
            return await PeticionPostPutAsync(HttpMethod.Put, uri, elemento, autorizacionToken, metodoAutorizacion);
        }


        private async Task<HttpResponseMessage> PeticionPostPutAsync<T>(HttpMethod metodo, string uri, T elemento, string autorizacionToken = null,
                                                                                                                            string autorizacionMetodo = "Bearer")
        {

            if(metodo != HttpMethod.Post && metodo != HttpMethod.Put)
            {

                throw new Excepcion($"Error en la peticion, solo se permiten hacer peticiones http Put o Post {nameof(metodo)}");

            }

            var request = new HttpRequestMessage(metodo, uri);

            request.Content = new StringContent(JsonConvert.SerializeObject(elemento), Encoding.UTF8, "application/json");

            if(autorizacionToken != null)
            {

                request.Headers.Authorization = new AuthenticationHeaderValue(autorizacionMetodo, autorizacionToken);

            }

            var respuesta = await _httpClient.SendAsync(request);

            if( respuesta.StatusCode == HttpStatusCode.InternalServerError )
            {

                throw new HttpRequestException();

            }

            return respuesta;

        }

        private void AutorizacionCabecera(HttpRequestMessage request)
        {

            var autorizacionCabecera = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(autorizacionCabecera))
            {

                request.Headers.Add("Authorization", new List<string> { autorizacionCabecera });

            }

        }

    }
}
