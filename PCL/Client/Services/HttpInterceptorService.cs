using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Toolbelt.Blazor;

namespace PCL.Client.Services
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor interceptor;
        private readonly NavigationManager navManager;

        public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navManager)
        {
            this.interceptor = interceptor;
            this.navManager = navManager;
        }

        public void MonitorEvent() => interceptor.AfterSend += interceptResponse;

        private void interceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            throw new NotImplementedException();
            
            string message = string.Empty;
            if (!e.Response.IsSuccessStatusCode)
            {

                var responseCode = e.Response.StatusCode;

                switch (responseCode)
                {
                    case HttpStatusCode.NotFound:
                        navManager.NavigateTo("/404");
                        message = "The requested resource was not found";
                        break;

                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.Forbidden:
                        navManager.NavigateTo("/unauthorized");
                        message = "You are not authorized to access this  resourcee.";
                        break;
                    default:
                        navManager.NavigateTo("/500");
                        message = "Something went wrong, please contact administrator";
                        break;
                }
            }
        }
        public void DisposeEvent() => interceptor.AfterSend -= interceptResponse;
    }
}
