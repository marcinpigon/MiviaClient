using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Interfaces
{
    public interface ISnackbarService
    {
        Task ShowSuccessSnackbarAsync(string message, string? filePath = null, int durationMs = 3000);
        Task ShowErrorSnackbarAsync(string message, int durationMs = 3000);
    }
}
