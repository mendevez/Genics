using genics.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace genics.Dtos
{
    public class AuthResponse
    {
        public UserReadDto User { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
