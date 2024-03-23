using SynCore.Core.Entities.Common;

namespace SynCore.Core.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public string CollegeName { get; set; }
}