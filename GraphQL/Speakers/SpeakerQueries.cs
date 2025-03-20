using ConferencePlanner.GraphQL.Data;
using GreenDonut.Data;
using HotChocolate.Execution.Processing;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Speakers;

[QueryType]
public static class SpeakerQueries
{
    [UsePaging]
    public static IQueryable<Speaker> GetSpeakersAsync(ApplicationDbContext dbContext)
    {
        return dbContext.Speakers.AsNoTracking().OrderBy(s => s.Name).ThenBy(s => s.Id);
    }

    [NodeResolver]
    public static async Task<Speaker?> GetSpeakerByIdAsync(
        int id,
        ISpeakerByIdDataLoader speakerById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await speakerById.Select(selection).LoadAsync(id, cancellationToken);
    }
}