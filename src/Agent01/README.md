# Agent01 - Microsoft Foundry Agent

A .NET 10 console application that implements an agent using Microsoft Agent Framework, built and orchestrated using Microsoft Foundry.

## Configuration

The application uses `appsettings.json` for configuration. The configuration includes:

- **ProjectEndpoint**: The Azure AI Projects endpoint URL
- **ApiKey**: The API key for authentication

### Setting up the API Key

For development, you can set the API key in one of the following ways:

1. **Using appsettings.Development.json** (recommended for local development):
   - Copy `appsettings.Development.json.example` to `appsettings.Development.json`
   - Add your API key to the `appsettings.Development.json` file:
   ```json
   {
     "AzureAI": {
       "ProjectEndpoint": "https://impnd01.services.ai.azure.com/api/projects/project01",
       "ApiKey": "your-api-key-here"
     }
   }
   ```
   - This file is gitignored and won't be committed to source control

2. **Using environment variables**:
   - Set the environment variable: `AzureAI__ApiKey=your-api-key-here`

3. **Using User Secrets** (for development):
   ```bash
   dotnet user-secrets set "AzureAI:ApiKey" "your-api-key-here"
   ```

## Building and Running

### Build the project:
```bash
dotnet build
```

### Run the project:
```bash
dotnet run
```

## Project Structure

- `Program.cs`: Main application entry point with agent initialization logic
- `appsettings.json`: Configuration template (no secrets)
- `appsettings.Development.json`: Development configuration (gitignored, contains API key)
- `Agent01.csproj`: Project file with dependencies

## Dependencies

- **Azure.AI.Projects** (1.2.0-beta.4): Azure AI Projects SDK for Microsoft Agent Framework
- **Microsoft.Extensions.Configuration**: Configuration framework
- **Microsoft.Extensions.Configuration.Json**: JSON configuration provider
- **Microsoft.Extensions.Configuration.Binder**: Configuration binding

## Features

- Loads configuration from appsettings.json
- Initializes Azure AI Projects client
- Configures agent for Microsoft Foundry orchestration
- Ready for extension with specific agent logic and workflows
