using MiTramite_Shared.DTOs.RentistaDTOs;

namespace WAMiTramite.Services;

public static class RentistaStateStore
{
    private static readonly object _syncLock = new();
    private static RentistaCurrentDataDTO? _current;

    public static RentistaCurrentDataDTO? Current
    {
        get
        {
            lock (_syncLock)
            {
                return _current;
            }
        }
    }

    public static void Set(RentistaCurrentDataDTO? rentista)
    {
        lock (_syncLock)
        {
            _current = rentista;
        }
    }

    public static void Clear()
    {
        Set(null);
    }
}
