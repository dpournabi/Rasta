using Microsoft.AspNetCore.Http;
using net.sf.mpxj;
using net.sf.mpxj.MpxjUtilities;
using net.sf.mpxj.reader;
using Rasta.ProjectManagment.Application.Common.Interfaces;

namespace Rasta.ProjectManagment.Application.Common.FileReaders.Providers;

public class MsProjectReader : FileReaderAbstraction
{
    private readonly List<Domain.Entities.ProjectWorkBreakdown> _excelData;
    public MsProjectReader(IApplicationDbContext context, IDateTimeService dateTimeService) :
        base(context, dateTimeService)
    {
        _excelData = new List<Domain.Entities.ProjectWorkBreakdown>();
    }

    public async override Task<List<Domain.Entities.ProjectWorkBreakdown>> ReadData(int projectId, IFormFile file)
    {
        _excelData.Clear();
        byte[] data = await ConvertIFormFileToByteArray(file);
        string fullPath = await ConvertByteArrayToFile(data, "mpp");
        UniversalProjectReader reader = new();
        ProjectFile project = reader.read(fullPath);
        CustomFieldContainer container = project.CustomFields;

        java.util.ArrayList childTasks = (java.util.ArrayList)project.ChildTasks;
        await System.Threading.Tasks.Task.WhenAll(ReadAllNodesRecursive(childTasks, projectId));
        return _excelData.OrderBy(x => x.Level).ToList();
    }

    public async System.Threading.Tasks.Task ReadAllNodesRecursive(java.util.ArrayList nodes, int projectId)
    {

        foreach (object? node in nodes)
        {
            net.sf.mpxj.Task task = (net.sf.mpxj.Task)node;
            int level = Convert.ToInt32(task.OutlineLevel.toString());

            string? strFloor = task.Get(TaskField.TEXT6)?.ToString();
            string? _floor = strFloor switch
            {
                "بام" => "100",
                "همه" => "101",
                _ => strFloor
            };
            int floor = 0;
            int.TryParse(_floor, out floor);

            Domain.Entities.ProjectWorkBreakdown newData = new();
            newData.CurrentTaskId = task.ID.intValue();
            newData.ParentTaskId = task.ParentTask is not null ? task.ParentTask.ID.intValue() : null;
            newData.ProjectId = projectId;
            newData.BaselineCost = task.BaselineCost.ToDecimal();
            newData.Floor = floor;
            newData.Budjet = task.BudgetCost.ToDecimal();
            newData.IsCritical = task.Critical;
            newData.WorkBreakdownStructureCode = task.WBS;
            newData.Duration = ((int)task.Duration.Duration);
            newData.Title = task.Name;
            //newData.//Successors = string.Join(';'; task.Successors.stream().toArray());
            //newData.//Predecessors = string.Join(';'; task.Predecessors.stream().toArray());
            newData.StartDate = task.Start.ToDateTime();
            newData.EndDate = task.Finish.ToDateTime();
            newData.LastStartDate = task.LateStart.ToDateTime();
            newData.LastEndDate = task.LateFinish.ToDateTime();
            newData.IsLastNode = !task.ChildTasks.toArray().Any();
            newData.Level = level;

            _excelData.Add(newData);

            await ReadAllNodesRecursive((java.util.ArrayList)task.ChildTasks, projectId);
        }
    }
}
