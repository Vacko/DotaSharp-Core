using System;

namespace DotaSharp
{
    [Flags]
    public enum EFontFlags
    {
        FontflagNone,
        FontflagItalic = 0x001,
        FontflagUnderline = 0x002,
        FontflagStrikeout = 0x004,
        FontflagSymbol = 0x008,
        FontflagAntialias = 0x010,
        FontflagGaussianblur = 0x020,
        FontflagRotary = 0x040,
        FontflagDropshadow = 0x080,
        FontflagAdditive = 0x100,
        FontflagOutline = 0x200,
        FontflagCustom = 0x400, // custom generated font - never fall back to asian compatibility mode
        FontflagBitmap = 0x800 // compiled bitmap font - no fallbacks
    }
}