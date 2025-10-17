using Rasta.ProjectManagment.Application.Common.Interfaces;
using System.Collections;

namespace Rasta.ProjectManagment.Application.Common.LocalizationManager
{
    public class ResourceManager: IResourceManager
    {
        public string? GetResxNameByValue(string key)
        {
            string value = null;
            System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("Rasta.ProjectManagment.Application.Common.LocalizationManager.Resources.Fa-Resource", typeof(ResourceManager).Assembly);
            return resourceManager.GetString(key);
        }
    }
}
