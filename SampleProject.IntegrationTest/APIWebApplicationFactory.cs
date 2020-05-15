using Microsoft.AspNetCore.Mvc.Testing;
using SampleProject.WebApi;

namespace SampleProject.IntegrationTest
{
    public class APIWebApplicationFactory : WebApplicationFactory<Startup>
    {
        // You can setup inmemory database here
    }
}