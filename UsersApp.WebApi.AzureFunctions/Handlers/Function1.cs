using AzureFromTheTrenches.Commanding.Abstractions;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UsersApp.WebApi.AzureFunctions.Commands;
using UsersApp.BLL.DTOs.Users;

namespace UsersApp.WebApi.AzureFunctions
{
    public class GetUserCommandHandler : ICommandHandler<GetUserCommand, UserDto>
    {
        [FunctionName("Function1")]
        public static async Task<string> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return "Hello azure functions!!!";
        }

        public Task<UserDto> ExecuteAsync(GetUserCommand command, UserDto previousResult)
        {
            throw new System.NotImplementedException();
        }
    }
}
