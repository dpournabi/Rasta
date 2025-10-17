using System;
using System.Collections.Generic;

namespace Infrastructure;

/// <summary>
/// تنظیمات فایلها
/// </summary>
public partial class ArchiveItemSetting
{
    public int Id { get; set; }

    /// <summary>
    /// پسوند فایل بدون نقطه. مثلا png, jpg, pdf
    /// </summary>
    public string? FileExtension { get; set; }

    /// <summary>
    /// آیکن معادل هر پسوند در قالب تگ html
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// حداکثر حجم قابل قبول برای هر نوع فایل-اگر null باشد یعنی محدودیتی نداریم.
    /// </summary>
    public long? MaxUploadLimit { get; set; }
}
