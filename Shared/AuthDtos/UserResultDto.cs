using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthDtos
{
    public record UserResultDto(int UserId, string Username, string Email, string Role ,string Token);
}
