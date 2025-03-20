using ConferencePlanner.GraphQL.Data;

namespace ConferencePlanner.GraphQL.Speakers;

public sealed class AddSpeakerPayload(Speaker speaker)
{
    public Speaker Speaker { get; } = speaker;
}