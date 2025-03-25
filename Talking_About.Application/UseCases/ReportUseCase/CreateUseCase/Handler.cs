using Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;
using Talking_About.Domain.Entities;
using Talking_About.Domain.Repositories;
using Talking_About.SharedKernel;

namespace Talking_About.Application.UseCases.ReportUseCase.CreateUseCase;

public sealed class CreateReportHandler : ICommandHandler<CreateReportCommand, Guid>
{
    private readonly IReportRepository _reportRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateReportHandler(IReportRepository reportRepository, IHttpContextAccessor httpContextAccessor)
    {
        _reportRepository = reportRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<Guid>> Handle(CreateReportCommand command, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user is null || !user.Identity?.IsAuthenticated == true)
        {
            throw new InvalidOperationException("Usuário não autenticado");
        }

        var userName = user.Identity.Name;

        var report = new Report(
            command.ReportName,
            command.TypeReport,
            command.ReportDescription,
            DateTime.UtcNow,
            userName!,
            command.IsEvent
        );

        await _reportRepository.AddAsync(report);

        return Result.Success(report.Id);
    }
}
