
   using System;
   using System.Net.Http;
   using System.Threading.Tasks;

   public class AsyncApi
   {
      private readonly HttpClient _client;

      public AsyncApi(string endpoint)
      {
         _client = new HttpClient {BaseAddress = new Uri(endpoint)};
      }

      public async Task<string> Get(string path)
      {
         return await _client.GetStringAsync(path);
      }
      
      
   }
