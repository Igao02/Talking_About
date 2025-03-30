using Application.Abstractions.Messaging;

namespace Talking_About.Application.UseCases.ReportUseCase.CreateUseCase;

public sealed record CreateReportCommand(string ReportName, string TypeReport, string ReportDescription, string UserName, bool IsEvent)
    : ICommand<Guid>;
