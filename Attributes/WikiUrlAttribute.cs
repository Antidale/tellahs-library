namespace tellahs_library.Attributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class WikiUrlAttribute(string url) : Attribute
{
    readonly string _url = url;

    public string Url => _url;
}
