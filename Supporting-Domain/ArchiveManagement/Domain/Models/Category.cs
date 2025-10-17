using System;
using System.Collections.Generic;

namespace Infrastructure;

/// <summary>
/// دسته بندی فایل ها
/// </summary>
public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool? Active { get; set; }

    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// ظرفیت حجمی هر دسته بندی بر حسب بایت
    /// </summary>
    public long? MaxCapacity { get; set; }

    public virtual ICollection<ArchiveItem> ArchiveItems { get; set; } = new List<ArchiveItem>();
}
