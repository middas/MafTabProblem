using MAF_Contracts;
using MAF_PluginView;
using System.AddIn.Contract;
using System.AddIn.Pipeline;

namespace MAF_PluginAdapter
{
    [AddInAdapter]
    internal class TabProblemViewToContractPluginSideAdapter : ContractBase, ITabProblemContract
    {
        private readonly ITabProblem view;

        public TabProblemViewToContractPluginSideAdapter(ITabProblem view)
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