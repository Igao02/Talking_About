using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Talking_About.Data;
using Talking_About.Domain.Entities;
using Talking_About.Domain.Repositories;

namespace Talking_About.Application.UseCases.ReportUseCase.CreateUseCase;

public sealed class CreateReportHandler
{
    private readonly IReportRepository _reportRepository;
    private readonly AuthenticationStateProvider _authentiactionStateProvider;

    public CreateReportHandler(IReportRepository reportRepository, AuthenticationStateProvider authentiactionStateProvider, UserManager<ApplicationUser> userManager)
    {
        _reportRepository = reportRepository;
        _authentiactionStateProvider = authentiactionStateProvider;
    }

    public async Task<Response> Handle(CreateReportCommand command)
    {
        var authState = await _authentiactionStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is null || !user.Identity.IsAuthenticated)
        {
            throw new InvalidOperationException("Usuário não autenticado");
        }

        var userName = user.Identity.Name;
        

        var report = new Report(command.ReportName, command.TypeReport, command.ReportDescription, DateTime.UtcNow, userName!, command.IsEvent);

        await _reportRepository.AddAsync(report);

        return new Response(report.Id);
    }

}
