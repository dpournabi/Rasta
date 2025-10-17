using System;
using System.Collections.Generic;

namespace Rasta.ProjectManagment.Domain.Entities;

/// <summary>
/// صورو وضعیت های مالی هر پروژه
/// </summary>
public partial class FinancialStatement: BaseEntity<int>
{
    /// <summary>
    /// شناسه پروژه
    /// </summary>
    public int ProjectId { get; set; }

    /// <summary>
    /// صورت وضعیت ارسالی تجمعی
    /// </summary>
    public long IntegratedSent { get; set; }

    /// <summary>
    /// صورت وضعیت ارسالی دوره
    /// </summary>
    public long PeriodSent { get; set; }

    /// <summary>
    /// تاریخ صورت وضعیت ارسالی
    /// </summary>
    public DateTime? SentDate { get; set; }

    /// <summary>
    /// صورت وضعیت تأیید شده تجمعی
    /// </summary>
    public long IntegratedApproved { get; set; }

    /// <summary>
    /// صورت وضعیت تأیید شده دوره
    /// </summary>
    public long PeriodApproved { get; set; }

    /// <summary>
    /// تاریخ تأیید صورت وضعیت 
    /// </summary>
    public DateTime? ApprovedDate { get; set; }

    /// <summary>
    /// تعدیل صورت وضعیت تأیید شده تجمعی
    /// </summary>
    public long BalancedIntegratedApproved { get; set; }

    /// <summary>
    /// تعدیل صورت وضعیت تأیید شده دوره
    /// </summary>
    public long BalancedPeriodApproved { get; set; }

    /// <summary>
    /// تاریخ تعدیل
    /// </summary>
    public DateTime? BalancedDate { get; set; }

    /// <summary>
    /// سال عملکرد
    /// </summary>
    public int OperationYear { get; set; }

    /// <summary>
    /// دوره عملکرد برحسب ماه
    /// </summary>
    public short OperationMonth { get; set; }

    /// <summary>
    /// تاریخ ایجاد سیستمی
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// تأیید شده یا نه
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// شناسه وضعیت مربوط به workflow
    /// </summary>
    public int ActionId { get; set; }

    public DateTime? DeleteDate { get; set; }
}
