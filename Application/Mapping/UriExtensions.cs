namespace Application.Mapping;

public static class UriExtensions
{
    public static string GetResourceIdFromUri(this Uri uri) 
    {
        if (!uri.Segments.Any()) 
        {
            throw new ArgumentException("Uri doesn't contain resource id");
        }

        return uri.Segments.Last().TrimEnd('/');
    }
}