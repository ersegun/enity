# Implementation Notes

## Changes Made

### Service Implementations
- **ConversionService** - Converts account balances using exchange rates with currency validation
- **AccountService** - Fetches account data from external API with HTTP client
- **ExchangeRateService** - Fetches exchange rates from external API
- **TransactionService** - Calculates highest positive balance change using O(n) algorithm

### Infrastructure
- **Logging** - Added warning/error level logging for troubleshooting
- **Exception Handling** - Comprehensive try-catch with meaningful error messages
- **Configuration** - Moved URLs to appsettings.json for environment-specific configuration
- **Dependency Injection** - Registered all services in Startup.cs with proper lifetimes
- **Swagger** - Added OpenAPI documentation at root URL

## Design Choices

1. **Options Pattern** - Used `IOptions<ServiceUrlsOptions>` for strongly-typed configuration
2. **HttpClientFactory** - Leveraged `AddHttpClient<TInterface, TImplementation>` for proper HTTP client lifecycle management
3. **Minimal Logging** - Only warnings and errors to reduce noise while capturing issues
4. **Swagger at Root** - Configured Swagger UI as landing page for better developer experience

## Possible Enhancements

- **Caching** - Add in-memory or distributed caching for exchange rates
- **Retry Policies** - Implement Polly for transient fault handling
- **Rate Limiting** - Protect external API calls from overuse
- **Validation** - Add FluentValidation for request parameter validation
- **Health Checks** - Add `/health` endpoint for monitoring
- **API Versioning** - Support multiple API versions
- **Authentication** - Add JWT or API key authentication
- **Circuit Breaker** - Prevent cascading failures with circuit breaker pattern
- **Metrics** - Add Prometheus/OpenTelemetry for observability
- **Unit Test Coverage** - Add tests for edge cases and error scenarios
