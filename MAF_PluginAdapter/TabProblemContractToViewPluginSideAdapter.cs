using MAF_Contracts;
using MAF_PluginView;
using System.AddIn.Pipeline;
using System.Windows;

namespace MAF_PluginAdapter
{
    internal class TabProblemContractToViewPluginSideAdapter : ITabProblem
    {
        private readonly ITabProblemContract contract;
        private readonly ContractHandle handle;

        public TabProblemContractToViewPluginSideAdapter(ITabProblemContract contract)
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