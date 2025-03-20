using ConferencePlanner.GraphQL.Data;
using GreenDonut.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Attendees;

public static class AttendeeDataLoaders
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Attendee>> AttendeeByIdAsync(
        IReadOnlyList<int> ids,
        ApplicationDbContext context,
        ISelectorBuilder selector,
        CancellationToken cancellationToken)
    {
        return await context.Attendees
            .AsNoTracking()
            .Where(a => ids.Contains(a.Id))
            .Select(a => a.Id, selector)
            .ToDictionaryAsync( a => a.Id, cancellationToken);
    }

    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Session[]>> SessionsByAttendeeIdAsync(
        IReadOnlyList<int> ids,
        ApplicationDbContext context,
        ISelectorBuilder selector,
        CancellationToken cancellationToken
    )
    {
        return await context.Sessions
            .AsNoTracking()
            .Where(a => ids.Contains(a.Id))
            .Select(s => s.Id, a => a.SessionAttendees.Select(b => b.Session), selector)
            .ToDictionaryAsync( r => r.Key, r => r.Value.ToArray(), cancellationToken);
    }
}