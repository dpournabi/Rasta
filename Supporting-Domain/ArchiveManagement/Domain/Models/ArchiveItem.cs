using System;
using System.Collections.Generic;

namespace Infrastructure;

/// <summary>
/// مشخصات فایل یا فولدر
/// </summary>
public partial class ArchiveItem
{
    public long Id { get; set; }

    /// <summary>
    /// دسته بندی
    /// </summary>
    public int? CategoryId { get; set; }

    /// <summary>
    /// شناسه آرشیو پدر که در اصل میشود پوشه ای که این آرشیو داخل آن قرار دارد
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// فایل فشرده شده است یا نه
    /// </summary>
    public bool? IsZiped { get; set; }

    /// <summary>
    /// اندازه فایل بر حسب بایت
    /// </summary>
    public long? Size { get; set; }

    /// <summary>
    /// عنوان فایل
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// نوع آرشیو: file یا folder
    /// </summary>
    public string? Type { get; set; }

    public string? MinIoUrl { get; set; }

    public string? FileExtension { get; set; }

    public string? FileMimeType { get; set; }

    public DateTime? CreateDate { get; set; }

    public long? CreatorId { get; set; }

    public long? ProjectId { get; set; }
    public DateTime? DeleteDate { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<ArchiveItem> InverseParent { get; set; } = new List<ArchiveItem>();

    public virtual ArchiveItem? Parent { get; set; }
}
