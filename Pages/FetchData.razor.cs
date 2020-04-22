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

        private TodoItem SelectedTodoItem { get; set; } = new TodoItem();

        protected override async Task OnInitializedAsync()
        {
            _todoItems = new List<TodoItem>();
            _todoItems.Add(new TodoItem { Id = 1, Name = "Rajesh", IsComplete = false });
            _todoItems.Add(new TodoItem { Id = 2, Name = "singh", IsComplete = true });

            //var request = new HttpRequestMessage(HttpMethod.Get,
            //"https://localhost:44387/api/TodoItems/GetAllTodoItems");
            //request.Headers.Add("Accept", "application/json");
            ////request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            //var client = httpClientFactory.CreateClient();
            //var response = client.SendAsync(request).Result;

            //if (response.IsSuccessStatusCode)
            //{
            //    using var responseStream = await response.Content.ReadAsStreamAsync();
            //    _todoItems = await JsonSerializer.DeserializeAsync
            //        <List<TodoItem>>(responseStream);
            //}
        }

        public void SaveFormData()
        {

        }

        public void EditData()
        {

        }

        public void EditData(TodoItem todoItem)
        {
            SelectedTodoItem = todoItem;
        }
    }
}
