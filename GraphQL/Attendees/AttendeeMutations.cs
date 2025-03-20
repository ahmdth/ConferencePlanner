using ConferencePlanner.GraphQL.Data;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Attendees;

[MutationType]
public class AttendeeMutations
{
    [Mutation]
    public static async Task<Attendee> RegisterAttendeeInput(
        RegisterAttendeeInput input,
        ApplicationDbContext context,
        CancellationToken cancellationToken
    )
    {
        var attendee = new Attendee
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Username = input.Username,
            EmailAddress = input.EmailAddress,
        };
        context.Attendees.Add(attendee);
        await context.SaveChangesAsync(cancellationToken);
        return attendee;
    }

    [Mutation]
    public static async Task<Attendee?> CheckInAttendeeAsync(
        CheckInAttendeeInput input,
        ApplicationDbContext context,
        ITopicEventSender eventSender,
        CancellationToken cancellationToken)
    {
        var attendee = await context.Attendees.FirstOrDefaultAsync(
            a => a.Id == input.AttendeeId,
            cancellationToken
        );
        if (attendee is null)
        {
            throw new AttendeeNotFoundException();
        }

        attendee.SessionsAttendees.Add(new SessionAttendee { SessionId = input.SessionId });
        await context.SaveChangesAsync(cancellationToken);
        await eventSender.SendAsync(
            $"OnAttendeeCheckedIn_{input.SessionId}",
            input.AttendeeId,
            cancellationToken);
        return attendee;
    }
}