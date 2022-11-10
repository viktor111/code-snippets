using Nager.PublicSuffix;

public static class UrlHelper
{
    public static string GetPathForBlobStorage(string url)
    {
        var uri = new Uri(url);
        var arr = uri.AbsolutePath.Split('/');
        return $"{arr[2]}/{arr[3]}/{arr[4]}";
    }
    public static string GetPath(string url)
    {
        var uri = new Uri(url);
        var path = uri.AbsolutePath;
        return path;
    }
    public static string GetDomain(string url)
    {
        var uri = new Uri(url);
        var dp = new DomainParser(new WebTldRuleProvider());
        var parsed = dp.Parse(uri);
        return parsed.Domain;
    }

    public static string GetHost(string url)
    {
        var uri = new Uri(url);
        var dp = new DomainParser(new WebTldRuleProvider());
        var parsed = dp.Parse(uri);
        return parsed.Hostname;
    }

    public static string GetSubDomain(string url)
    {
        var uri = new Uri(url);
        var dp = new DomainParser(new WebTldRuleProvider());
        var parsed = dp.Parse(uri);
        return parsed.SubDomain;
    }
}