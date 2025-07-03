using System;
using System.Threading.Tasks;

namespace CommonLib.Contracts;

public interface IAppHostStartup
{
    /// <summary>
    /// A method that is executed when a <see cref="IApplication"/> starts.
    /// 
    /// Use this to perform initialization code that executes when the application starts,
    /// before the splash screen is shown.
    /// </summary>
    /// <remarks>
    /// By throwing a <see cref="StartupException"/>, application startup can be aborted with a specified error message.
    /// By throwing a <see cref="OperationCanceledException"/>, application startup can be aborted.
    /// </remarks>
    /// <returns>A task that signals completion of the asynchronous operation.</returns>
    Task Startup();
}