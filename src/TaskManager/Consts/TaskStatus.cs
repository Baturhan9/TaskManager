using Microsoft.CodeAnalysis.CSharp.Syntax;
using TaskManager.Exceptions.RequestExceptions;

namespace TaskManager.Consts
{
    public enum TaskStatus
    {
        New,
        InProgress,
        NeedReview,
        NeedTesting,
        Tested,
        Completed,
        Canceled,
        OnHold,

        /// <summary>
        /// This is for comment and other changes in system.
        /// </summary>
        Empty,
    }
}
