using Azure.AI.Projects;
using Azure.AI.Projects.OpenAI;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using OpenAI.Responses;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var projectEndpoint = configuration["AzureAI:ProjectEndpoint"];
var apiKey = configuration["AzureAI:ApiKey"];
var agentName = configuration["AzureAI:AgentName"];

if (string.IsNullOrEmpty(projectEndpoint) || string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("Error: ProjectEndpoint or ApiKey not found in configuration.");
    return;
}

Console.WriteLine($"Initializing agent {agentName}...");

try
{
    var endpoint = new Uri(projectEndpoint);
    AIProjectClient projectClient = new(endpoint: new Uri(projectEndpoint), tokenProvider: new DefaultAzureCredential());
    AgentRecord agentRecord = projectClient.Agents.GetAgent(agentName);
    Console.WriteLine($"Agent retrieved (name: {agentRecord.Name}, id: {agentRecord.Id})");

    Console.WriteLine("\nType a message to send to the agent (or press Enter to exit):");
    var input = Console.ReadLine();

    OpenAIResponseClient responseClient = projectClient.OpenAI.GetProjectResponsesClientForAgent(agentRecord);
    OpenAIResponse response = responseClient.CreateResponse(input);
    Console.WriteLine(response.GetOutputText());
}
catch (Exception ex)
{
    Console.WriteLine($"Error initializing agent: {ex.Message}");
    Console.WriteLine($"Stack trace: {ex.StackTrace}");
}

Console.WriteLine("Press Enter to exit.");
Console.ReadLine();