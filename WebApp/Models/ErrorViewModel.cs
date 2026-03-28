using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        
    }
}
