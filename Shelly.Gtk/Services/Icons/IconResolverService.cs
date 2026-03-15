namespace Shelly.Gtk.Services.Icons;

public class IconResolverService : IIconResolverService
{
    private static readonly string[] Repos = ["archlinux-arch-extra", "archlinux-arch-core", "archlinux-arch-multilib"];
    private static readonly string[] Sizes = ["128x128", "64x64", "48x48"];

    private const string IconPath = "/usr/share/swcatalog";
    private const string LegacyIconPath = "/usr/share/app-info";

    public string? GetIconPath(string packageName)
    {
        var path = GetBasePath();
        if (string.IsNullOrEmpty(path))
        {
            return "Unavailable";
        }

        return (from repo in Repos
                from size in Sizes
                select Path.Combine(path, "icons", repo, size)
                into dir
                where Directory.Exists(dir)
                select Directory.EnumerateFiles(dir, $"{packageName}_*.png").FirstOrDefault())
            .FirstOrDefault();
    }


    private string? GetBasePath() => Directory.Exists("/usr/share/swcatalog") ? IconPath :
        Directory.Exists(LegacyIconPath) ? LegacyIconPath : null;
}