using Blazor_ClientPortal.API;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorApp_ClientPortal.Pages
{
    public partial class FetchData
    {
        [Inject]
        public IHttpClientFactory httpClientFactory { get; set; }
        private List<TodoItem> _todoItems { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "https://localhost:44387/api/TodoItems/GetAllTodoItems");
            request.Headers.Add("Accept", "application/json");
            //request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                _todoItems = await JsonSerializer.DeserializeAsync
                    <List<TodoItem>>(responseStream);
            }
        }
    }
}
