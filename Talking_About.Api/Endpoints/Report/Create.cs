using MediatR;
using Talking_About.Api.Extensions;
using Talking_About.Api.Infrastructure;
using Talking_About.Application.UseCases.ReportUseCase.CreateUseCase;
using Talking_About.SharedKernel;

namespace Talking_About.Api.Endpoints.Reports;

public sealed class CreateReportEndpoint : IEndpoint
{
    public sealed record Request(string ReportName, string TypeReport, string ReportDescription, string UserName, bool IsEvent);

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/reports/create", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateReportCommand(
                request.ReportName,
                request.TypeReport,
                request.ReportDescription,
                request.UserName,
                request.IsEvent);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithName("CreateReport")
        .WithTags("Reports")
        .WithOpenApi();  

    }
}
