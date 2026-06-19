using Microsoft.AspNetCore.Components;

namespace NameBadge.Models;

public class ProgramType
{
    private ProgramType(string code, string displayName, string shortName)
    {
        Code = code;
        DisplayName = (MarkupString)displayName;
        ShortName = shortName;
    }
    
    public string Code { get; }
    public MarkupString DisplayName { get; }
    public string ShortName { get; }
    
    public static HashSet<ProgramType> ProgramTypes =>
    [
        new("FLL", "<i>FIRST</i><sup>&reg;</sup> LEGO<sup>&reg;</sup> League Challenge", "FLL Challenge"),
        new("JFLL", "<i>FIRST</i><sup>&reg;</sup> LEGO<sup>&reg;</sup> League Explore", "FLL Explore"),
        new("FTC", "<i>FIRST</i><sup>&reg;</sup> Tech Challenge", "FTC"),
        new("FRC", "<i>FIRST</i><sup>&reg;</sup> Robotics Competition", "FRC")
    ];
}