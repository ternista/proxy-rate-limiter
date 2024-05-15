namespace Application.Contract;

using Application.Model;

public interface IPeopleDataProvider
{
    /// <summary>
    /// Returns array of people by their ids.
    /// </summary>
    /// <param name="ids">Array of people identifiers.</param>
    Task<ICollection<Person>> GetPeople(IEnumerable<string> ids, CancellationToken cancellationToken);
    /// <summary>
    /// Returns single person by id.
    /// </summary>
    /// <param name="ids">Array of people identifiers.</param>
    Task<Person> GetPerson(string id, CancellationToken cancellationToken);
}