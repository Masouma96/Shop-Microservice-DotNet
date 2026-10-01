using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IDP.Application.Commands.User
{
    public class UserCommands:IRequest<bool>
    {
        [Required(ErrorMessage ="this data is required")]
        [MinLength(3)]

        public required string FullName { get; set; }
        public required string CodeNumber { get; set; }

    }
}
