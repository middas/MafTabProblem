using MAF_Contracts;
using MAF_HostView;
using System.AddIn.Contract;
using System.AddIn.Pipeline;

namespace MAF_HostAdapter
{
    internal class TabProblemViewToContractHostSideAdapter : ContractBase, ITabProblemContract
    {
        private readonly ITabProblem view;

        public TabProblemViewToContractHostSideAdapter(ITabProblem view)
        {
            this.view = view;
        }

        public void Dispose()
        {
            view.Dispose();
        }

        public INativeHandleContract GetWpfControl()
        {
            return FrameworkElementAdapters.ViewToContractAdapter(view.GetWpfControl());
        }

        internal ITabProblem GetSourceView()
        {
            return view;
        }
    }
}