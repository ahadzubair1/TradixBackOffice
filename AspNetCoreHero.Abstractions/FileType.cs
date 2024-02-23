using System.ComponentModel;

namespace AspNetCoreHero.Abstractions;

public enum FileType
{
    [Description(".jpg,.png,.jpeg")]
    Image
}