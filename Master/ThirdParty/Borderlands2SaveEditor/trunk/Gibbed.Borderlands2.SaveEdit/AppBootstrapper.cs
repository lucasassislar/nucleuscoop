/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using Caliburn.Micro.Contrib;

namespace Gibbed.Borderlands2.SaveEdit
{
    internal class AppBootstrapper : Bootstrapper<ShellViewModel>
    {
        private CompositionContainer _Container;

        protected override void Configure()
        {
            FrameworkExtensions.Message.Attach.AllowExtraSyntax(MessageSyntaxes.SpecialValueProperty |
                                                                MessageSyntaxes.XamlBinding);
            FrameworkExtensions.ActionMessage.EnableFilters();
            FrameworkExtensions.ViewLocator.EnableContextFallback();

            this._Container =
                new CompositionContainer(
                    new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x))));

            var batch = new CompositionBatch();
            batch.AddExportedValue<IWindowManager>(new AppWindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(this._Container);

            this._Container.Compose(batch);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return base.SelectAssemblies().Concat(new[]
            {
                typeof(ResultExtensions).Assembly
            });
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;

            var exports = this._Container.GetExportedValues<object>(contract).ToArray();
            if (exports.Length > 0)
            {
                return exports[0];
            }

            throw new InvalidOperationException(string.Format("Could not locate any instances of contract {0}.",
                                                              contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return this._Container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance)
        {
            this._Container.SatisfyImportsOnce(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);

            try
            {
                GameInfo.InfoManager.Touch();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An exception was thrown (press Ctrl+C to copy):\n\n" + ex,
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                Application.Shutdown(1);
            }
        }
    }
}
