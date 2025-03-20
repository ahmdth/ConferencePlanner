namespace ConferencePlanner.GraphQL.Speakers;

public sealed record UpdateSpeakerInput(
    string Name,
    string? Bio,
    string? Website);