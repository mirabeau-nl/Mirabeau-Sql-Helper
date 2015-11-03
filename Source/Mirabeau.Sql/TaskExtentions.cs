using System.Threading.Tasks;

namespace Mirabeau.Sql
{
    internal static class TaskExtentions
    {
        internal static T TaskResult<T>(this Task<T> task)
        {
            return task.GetAwaiter().GetResult();
        }
    }
}