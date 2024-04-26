using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SynCore.Api.Common.Exceptions;
using SynCore.Api.Data;
using SynCore.Core.Entities;

namespace SynCore.Api.Features.Classes;

public static class AddClass
{
    public class Command : IRequest<Class>
    {
        public string Name { get; set; }
        public int Absences { get; set; }
        public int Total { get; set; }
        
        public Guid UserId { get; set; }
        public Time[] Times { get; set; }

        public class Time
        {
            public DayOfWeek DayOfWeek { get; set; }
            
            public string StartHour { get; set; }
            public string StartMinute { get; set; }
    
            public string EndHour { get; set; }
            public string EndMinute { get; set; }
        }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Nome não pode ser vazio");

            RuleFor(c => c.Total)
                .GreaterThanOrEqualTo(0).WithMessage("Total de aulas deve ser maior ou igual a 0");
            
            RuleFor(c => c.Absences)
                .GreaterThanOrEqualTo(0).WithMessage("Total de faltas deve ser maior ou igual a 0");
            
            RuleFor(c => c.Times)
                .NotNull().WithMessage("Matéria precisa de horário(s)")
                .NotEmpty().WithMessage("Matéria precisa de horário(s)");
        }
    }

    public class TimeValidator : AbstractValidator<Command.Time>
    {
        public TimeValidator()
        {
            RuleFor(t => t.DayOfWeek)
                .NotNull().WithMessage("Dia da semana não pode ser vazio");

            RuleFor(t => t.StartHour)
                .NotEmpty();

            RuleFor(t => t.StartMinute)
                .NotEmpty();

            RuleFor(t => t.EndHour)
                .NotEmpty();

            RuleFor(t => t.EndMinute)
                .NotEmpty();
        }
    }
    
    public class Handler : IRequestHandler<Command, Class>
    {
        private readonly IValidator<Command> _validator;
        private readonly IValidator<Command.Time> _timeValidator;
        private readonly AppDbContext _appDbContext;

        public Handler(
            IValidator<Command> validator, 
            IValidator<Command.Time> timeValidator, 
            AppDbContext appDbContext)
        {
            _validator = validator;
            _timeValidator = timeValidator;
            _appDbContext = appDbContext;
        }

        public async Task<Class> Handle(Command request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            foreach (var time in request.Times)
            {
                await _timeValidator.ValidateAndThrowAsync(time, cancellationToken);
            }

            var existClass = await _appDbContext.Classes
                .AsNoTracking()
                .AnyAsync(c => c.Name == request.Name, cancellationToken);

            if (existClass)
            {
                throw new AppException(StatusCodes.Status409Conflict, "matéria já cadastrada");
            }

            var newClass = request.Adapt<Class>();
            newClass.Id = Guid.NewGuid();
            newClass.IsActive = true;

            foreach (var time in newClass.Times)
            {
                time.ClassId = newClass.Id;
            }

            await _appDbContext.Classes.AddAsync(newClass, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return newClass;
        }
    }
}