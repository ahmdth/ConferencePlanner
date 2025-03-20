namespace ConferencePlanner.GraphQL.Tracks;

public sealed class NameEmptyException() : Exception("Track name can't be null or empty.");

public sealed class TrackNotFoundException() : Exception("Track not found.");
