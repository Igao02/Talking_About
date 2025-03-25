using MediatR;
using Talking_About.Api.Infrastructure;
using Talking_About.Application.UseCases.ReportUseCase.CreateUseCase;
using Talking_About.Common.Api;
using Talking_About.Extensions;
using Talking_About.SharedKernel;

namespace Talking_About.Api.Endpoints.Reports;

internal sealed class CreateReportEndpoint : IEndpoint
{
    public sealed record Request(string ReportName, string TypeReport, string ReportDescription, string UserName, bool IsEvent);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/reports/create", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            // Criação do comando com os dados recebidos
            var command = new CreateReportCommand(
                request.ReportName,
                request.TypeReport,
                request.ReportDescription,
                request.UserName,  // O nome do usuário pode ser recuperado da autenticação aqui
                request.IsEvent);

            // Explicitly cast the result to Result<Guid>
            Result<Guid> result = (Result<Guid>)await sender.Send(command, cancellationToken);

            // Retorna a resposta adequada
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags("Reports");// Ensure you have the necessary using directives
    }
}
