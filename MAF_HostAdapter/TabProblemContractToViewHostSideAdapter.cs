using MAF_Contracts;
using MAF_HostView;
using System.AddIn.Pipeline;
using System.Windows;

namespace MAF_HostAdapter
{
    [HostAdapter]
    internal class TabProblemContractToViewHostSideAdapter : ITabProblem
    {
        private readonly ITabProblemContract contract;
        private readonly ContractHandle handle;

        public TabProblemContractToViewHostSideAdapter(ITabProblemContract contract)
        {
            this.contract = contract;
            handle = new ContractHandle(contract);
        }

        public void Dispose()
        {
            contract.Dispose();
        }

        public FrameworkElement GetWpfControl()
        {
            return FrameworkElementAdapters.ContractToViewAdapter(contract.GetWpfControl());
        }

        internal ITabProblemContract GetSourceContract()
        {
            return contract;
        }
    }
}