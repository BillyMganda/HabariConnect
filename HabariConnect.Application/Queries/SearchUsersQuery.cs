using HabariConnect.Domain.DTOs.User;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HabariConnect.Application.Queries
{
    public class SearchUsersQuery : IRequest<IEnumerable<UserGetDto>>
    {
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value?.Trim();
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value?.Trim();
        }

        public bool HasNameQuery => !string.IsNullOrEmpty(FirstName) || !string.IsNullOrEmpty(LastName);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!HasNameQuery)
            {
                yield return new ValidationResult("At least one of FirstName or LastName is required.", new[] { nameof(FirstName), nameof(LastName) });
            }
        }
    }
}
