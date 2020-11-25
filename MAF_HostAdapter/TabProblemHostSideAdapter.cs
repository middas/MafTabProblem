using MAF_Contracts;
using MAF_HostView;
using System.Runtime.Remoting;

namespace MAF_HostAdapter
{
    internal static class TabProblemHostSideAdapter
    {
        internal static ITabProblem ContractToViewAdapter(ITabProblemContract contract)
        {
            if (((!RemotingServices.IsObjectOutOfAppDomain(contract))
                && contract.GetType().Equals(typeof(TabProblemViewToContractHostSideAdapter))))
            {
                return ((TabProblemViewToContractHostSideAdapter)contract).GetSourceView();
            }
            else
            {
                return new TabProblemContractToViewHostSideAdapter(contract);
            }
        }

        internal static ITabProblemContract ViewToContract(ITabProblem view)
        {
            if (view.GetType().Equals(typeof(TabProblemContractToViewHostSideAdapter)))
            {
                return ((TabProblemContractToViewHostSideAdapter)view).GetSourceContract();
            }
            else
            {
                return new TabProblemViewToContractHostSideAdapter(view);
            }
        }
    }
}