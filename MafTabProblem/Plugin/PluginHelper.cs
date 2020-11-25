using MAF_HostView;
using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MafTabProblem.Plugin
{
    internal class PluginHelper
    {
        private static readonly string pipelinePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Pipeline");
        private static bool addInStoreLoaded = false;

        public static void DeactivatePlugin(object plugin)
        {
            try
            {
                AddInController.GetAddInController(plugin).Shutdown();
            }
            catch (CannotUnloadAppDomainException)
            {
                Console.WriteLine("Could not unload plugin AppDomain");
            }
        }

        public static string GetTypeName(object plugin)
        {
            return GetAddInToken(plugin)?.AddInFullName;
        }

        public static string GetTypePublisher(object plugin)
        {
            return GetAddInToken(plugin)?.Publisher;
        }

        public static string GetTypeVersion(object plugin)
        {
            return GetAddInToken(plugin)?.Version;
        }

        public PluginScopeHelper<T> ActivateUninitializedPlugin<T>(string typeName, bool loadInCurrentAppDomain) where T : IDisposable
        {
            var plugins = GetRegisteredPluginsThatImplementInterface(typeof(T)).ToList();
            var token = plugins.FirstOrDefault(p => string.Equals(p.AddInFullName, typeName));

            if (token == null)
            {
                throw new ArgumentNullException(string.Format("No plugins found matching {0}", typeName));
            }

            return ActivateUninitializedPlugin<T>(token, loadInCurrentAppDomain);
        }

        public PluginScopeHelper<T> ActivateUninitializedPlugin<T>(AddInToken token, bool loadInCurrentAppDomain) where T : IDisposable
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (!loadInCurrentAppDomain)
            {
                // this works with tab navigation in the popup
                return new PluginScopeHelper<T>(token.Activate<T>(AddInSecurityLevel.FullTrust), token.Name, token.Publisher, token.Version);
            }
            else
            {
                // this does not
                return new PluginScopeHelper<T>(token.Activate<T>(AppDomain.CurrentDomain), token.Name, token.Publisher, token.Version);
            }
        }

        public IEnumerable<AddInToken> GetRegisteredPlugins()
        {
            return GetRegisteredPluginsThatImplementInterface(typeof(ITabProblem));
        }

        public IEnumerable<AddInToken> GetRegisteredPluginsThatImplementInterface(Type t)
        {
            LoadPluginStoreOnFirstAccess();
            return AddInStore.FindAddIns(t, pipelinePath);
        }

        private static AddInToken GetAddInToken(object plugin)
        {
            try
            {
                var ac = AddInController.GetAddInController(plugin);

                if (ac != null)
                {
                    return ac.Token;
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        private static void LoadPluginStoreOnFirstAccess()
        {
            if (!addInStoreLoaded)
            {
                UpdatePluginStore();
                addInStoreLoaded = true;
            }
        }

        private static void UpdatePluginStore()
        {
            Action<List<string>> LogWarnings = warns => warns.ForEach(warning =>
            {
                Console.WriteLine(warning);
            });

            var warnings = new List<string>(AddInStore.Rebuild(pipelinePath));
            LogWarnings(warnings);
            warnings.Clear();
        }
    }
}