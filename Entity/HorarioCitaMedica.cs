using Entity;
using System;

public class HorarioCitaMedica : Horario
{
    public DateTime FechaHora { get; set; }

    public static readonly TimeSpan DuracionDefecto = TimeSpan.FromMinutes(30);

    public TimeSpan Duracion = DuracionDefecto;

    public override TimeSpan HoraInicio
    {
        get => FechaHora.TimeOfDay;
        set => FechaHora = FechaHora.Date + value;
    }

    public override TimeSpan HoraFin
    {
        get => FechaHora.TimeOfDay + Duracion;
        set => Duracion = value - FechaHora.TimeOfDay;
    }
    public HorarioCitaMedica(DateTime fechaHora)
    {
        FechaHora = fechaHora;
    }
    public HorarioCitaMedica()
    {
    }
}
