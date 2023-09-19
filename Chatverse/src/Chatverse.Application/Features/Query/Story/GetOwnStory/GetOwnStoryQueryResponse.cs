namespace Chatverse.Application.Features.Query.Story.GetOwnStory;

public class GetOwnStoryQueryResponse
{
    public List<OwnStory>? Stories { get; set; }
}
public class OwnStory
{
    public string? ProfilePicture { get; set; }
    public string? Media { get; set; }
}
