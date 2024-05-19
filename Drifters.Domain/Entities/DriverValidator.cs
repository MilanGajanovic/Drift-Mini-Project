using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Drifters.Domain.Entities
{
    public class DriverValidator : AbstractValidator<Driver>
    {

        public DriverValidator()
            
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Nationality).NotEmpty().WithMessage("Nationality is required");
            RuleFor(x => x.ChampionshipsWon).NotEmpty().WithMessage("ChampionshipsWon is required");
        }
    }
}
