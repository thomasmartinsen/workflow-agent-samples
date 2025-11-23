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
var agentName = "translation-game";
var agentVersion = "3";

if (string.IsNullOrEmpty(projectEndpoint) || string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("Error: ProjectEndpoint or ApiKey not found in configuration.");
    return;
}

Console.WriteLine($"Initializing workflow {agentName}...");

try
{
    AIProjectClient projectClient = new(endpoint: new Uri(projectEndpoint), tokenProvider: new DefaultAzureCredential());

    ProjectConversation conversation = projectClient.OpenAI.Conversations.CreateProjectConversation();
    AgentReference agentReference = new AgentReference(name: agentName, version: agentVersion);
    ProjectResponsesClient responseClient = projectClient.OpenAI.GetProjectResponsesClientForAgent(agentReference, conversation.Id);

    Console.WriteLine("\nType a message to send to the agent (or press Enter to exit):");
    var input = Console.ReadLine();

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