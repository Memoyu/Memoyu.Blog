using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Domains.Common.Enums.Base;

namespace Blog.Service.Core.Auth.Output
{
    public class UserDto
    {
        public long Id { get; set; }

        public int UserId { get; set; }

        public int UserType { get; set; }

        public string UserTypeName { get; set; }

        public string AvatarUrl { get; set; }

        public string UserUrl { get; set; }

        public string Name { get; set; }

        public string Company { get; set; }

        public string Location { get; set; }

        public string Email { get; set; }

        public string Bio { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
