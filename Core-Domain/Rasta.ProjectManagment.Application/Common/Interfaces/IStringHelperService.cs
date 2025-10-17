namespace Rasta.ProjectManagment.Application.Common.Interfaces;

public interface IStringHelperService
{
    Task<string> ReplaceAllAsync(string text, char? replaceFrom, char? replaceTo=null);
}
