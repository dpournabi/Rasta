using Rasta.ProjectManagment.Application.Common.Interfaces;
using System.Text;

namespace Rasta.ProjectManagment.Infrastructure.Services;

public class StringHelperService : IStringHelperService
{
    public async Task<string> ReplaceAllAsync(string text, char? replaceFrom, char? replaceTo = null)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        var textParts = replaceFrom is not null ? text.Split(replaceFrom.Value) : text.Split(new char[] { '.', '/', '_' });
        char splitChar = replaceTo is not null ? replaceTo.Value : '-';
        StringBuilder stringBuilder = new StringBuilder();
        foreach (var part in textParts)
            stringBuilder.Append($"{part}{splitChar}");

        return await Task.FromResult(stringBuilder.ToString().TrimEnd('-'));
    }
}
