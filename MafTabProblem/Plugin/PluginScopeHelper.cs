using System;
using System.AddIn.Hosting;

namespace MafTabProblem.Plugin
{
    public sealed class PluginScopeHelper<T> : IDisposable where T : IDisposable
    {
        private readonly T plugin;

        private AddInController controller;
        private bool disposed = false;

        public PluginScopeHelper(T plugin, string name, string publisher, string version)
        {
            this.plugin = plugin;
            Name = name;
            Publisher = publisher;
            Version = version;

            try
            {
                controller = AddInController.GetAddInController(plugin);
            }
            catch (ArgumentException)
            {
                // ignore this error, dispose tries to find the controller again
            }
        }

        public string Name { get; }

        public T Plugin { get => plugin; }

        public string Publisher { get; }

        public string Version { get; }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                // clean managed objects
                try
                {
                    if (controller == null)
                    {
                        PluginHelper.DeactivatePlugin(plugin);
                    }
                    else
                    {
                        // check if the appdomain has been disposed already to prevent a deadlock
                        var test = controller.AppDomain.BaseDirectory;

                        controller.Shutdown();
                    }
                }
                catch (ObjectDisposedException)
                {
                    // the appdomain has been disposed, this is ok
                }
                catch (CannotUnloadAppDomainException ex)
                {
                    // this is bad
                    Console.WriteLine(ex);
                }
                catch (AppDomainUnloadedException)
                {
                    // the appdomain has already been unloaded, this is ok
                }
                catch (Exception ex)
                {
                    // really bad, unknown exception
                    Console.WriteLine(ex);
                }
                finally
                {
                    controller = null;
                }
            }

            // free unmananged resources
            // set large fields to null

            disposed = true;
        }
    }
}