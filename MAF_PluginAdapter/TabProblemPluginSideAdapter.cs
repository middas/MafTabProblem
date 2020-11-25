using MAF_Contracts;
using MAF_PluginView;
using System.Runtime.Remoting;

namespace MAF_PluginAdapter
{
    internal static class TabProblemPluginSideAdapter
    {
        internal static ITabProblem ContractToViewAdapter(ITabProblemContract contract)
        {
            if (((!RemotingServices.IsObjectOutOfAppDomain(contract))
                && contract.GetType().Equals(typeof(TabProblemViewToContractPluginSideAdapter))))
            {
                return ((TabProblemViewToContractPluginSideAdapter)contract).GetSourceView();
            }
            else
            {
                return new TabProblemContractToViewPluginSideAdapter(contract);
            }
        }

        internal static ITabProblemContract ViewToContractAdapter(ITabProblem view)
        {
            if (view.GetType().Equals(typeof(TabProblemContractToViewPluginSideAdapter)))
            {
                return ((TabProblemContractToViewPluginSideAdapter)view).GetSourceContract();
            }
            else
            {
                return new TabProblemViewToContractPluginSideAdapter(view);
            }
        }
    }
}