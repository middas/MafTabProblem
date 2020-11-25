using System;
using System.AddIn.Contract;
using System.AddIn.Pipeline;

namespace MAF_Contracts
{
    [AddInContract]
    public interface ITabProblemContract : IContract, IDisposable
    {
        INativeHandleContract GetWpfControl();
    }
}