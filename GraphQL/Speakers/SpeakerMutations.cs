using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Speakers;

namespace ConferencePlanner.GraphQL.Speakers;

[MutationType]
public static class SpeakerMutations
{
    [Mutation]
    public static async Task<Speaker> AddSpeakerAsync(
        AddSpeakerInput input,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var speaker = new Speaker
        {
            Name = input.Name,
            Bio = input.Bio,
            Website = input.Website
        };

        dbContext.Speakers.Add(speaker);

        await dbContext.SaveChangesAsync(cancellationToken);

        return speaker;
    }

    // public static async Task<Speaker> UpdateSpeakerAsync(
    //     int id,
    //     UpdateSpeakerInput input,
    //     ApplicationDbContext dbContext,
    //     CancellationToken cancellationToken
    // )
    // {
    //     var speaker = dbContext.Speakers.FirstOrDefault(speaker => speaker.Id == id);
    //     speaker.Name = input.Name;
    //     speaker.Bio = input.Bio;
    //     speaker.Website = input.Website;
    //     
    //     dbContext.Speakers.Add(speaker);
    //
    //     await dbContext.SaveChangesAsync(cancellationToken);
    //
    //     return speaker;
    // }
    
}