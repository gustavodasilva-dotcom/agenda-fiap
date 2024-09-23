namespace Agenda.Common.DependencyInjection.Options;

public sealed class KillSwitchOptions
{
    public int ActivationThreshold { get; set; }

    public double TripThreshold { get; set; }

    public int RestartMinutesTimeout { get; set; }
}
