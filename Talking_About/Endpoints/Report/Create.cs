using System.Security.Claims;
using Talking_About.Api.Endpoints;
using Talking_About.Application.UseCases.ReportUseCase.CreateUseCase;

namespace Talking_About.API.Endpoints.Report
{
    public class CreateReportEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/reports", HandleAsync)
                .WithName("Reports: Create")
                .WithSummary("Cria um novo relatório")
                .WithDescription("Cria um novo relatório de denúncia")
                .WithOrder(1)
                .Produces<Response>(); // Usa sua classe Response(Guid Id)
        }


        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            Map(app);
        }

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            CreateReportHandler handler,
            CreateReportCommand command)
        {
            var updatedCommand = command with { UserName = user.Identity?.Name ?? string.Empty }; // Captura o usuário autenticado

            // Chama o Handler para processar o comando
            try
            {
                var result = await handler.Handle(updatedCommand);

                // Retorna resultado com Created status
                return TypedResults.Created($"/reports/{result.Id}", result);
            }
            catch (InvalidOperationException)
            {
                // Retorna BadRequest caso o usuário não esteja autenticado
                return TypedResults.BadRequest("Usuário não autenticado.");
            }
        }
    }
}
