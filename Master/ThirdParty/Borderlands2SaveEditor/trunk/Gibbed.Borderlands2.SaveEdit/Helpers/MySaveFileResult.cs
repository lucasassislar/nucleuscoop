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
using System.IO;
using Caliburn.Micro;
using Caliburn.Micro.Contrib.Helper;
using Microsoft.Win32;

namespace Gibbed.Borderlands2.SaveEdit
{
    // The original SaveFileResult continues execution in FileOk, which is bad
    // since that can execute before the dialog has closed.

    /// <summary>
    /// A custom Caliburn.Micro result which shows a SaveFileDialog to the user.
    /// You can also directly perform an action on the selected file inside the result
    /// </summary>
    public class MySaveFileResult : IResult
    {
        private readonly FileFilterCollection _Filters = new FileFilterCollection();
        private readonly string _Title;
        private EventHandler<ResultCompletionEventArgs> _Completed = delegate { };
        private Action<string> _FileAction;
        private bool _IgnoreUserCancel;
        private string _InitialDirectory;
        private bool _PromptForCreate;
        private bool _PromptForOverwrite;

        /// <summary>
        /// Creates a new SaveFileResult.
        /// </summary>
        /// <param name="title">The title of the dialog window</param>
        public MySaveFileResult(string title = null)
        {
            this._Title = title;
        }

        /// <summary>
        /// The name of the file selected by the user.
        /// </summary>
        public string FileName { get; private set; }

        #region IResult Members
        void IResult.Execute(ActionExecutionContext context)
        {
            var dialog = CreateDialog();

            if (dialog.ShowDialog() != true)
            {
                this._Completed(this,
                                new ResultCompletionEventArgs
                                {
                                    WasCancelled = !this._IgnoreUserCancel
                                });
                return;
            }

            var resultArgs = new ResultCompletionEventArgs();

            this.FileName = dialog.FileName;

            if (this._FileAction != null)
            {
                try
                {
                    this._FileAction(FileName);
                }
                catch (Exception e)
                {
                    resultArgs.Error = e;
                }
            }

            this.OnCompleted(resultArgs);
        }

        event EventHandler<ResultCompletionEventArgs> IResult.Completed
        {
            add { this._Completed += value; }
            remove { this._Completed -= value; }
        }
        #endregion

        private void OnCompleted(ResultCompletionEventArgs args)
        {
            this._Completed(this, args);
        }

        /// <summary>
        /// Creates the dialog with the user specified settings.
        /// Can be overridden to change the default settings
        /// </summary>
        /// <returns></returns>
        protected virtual SaveFileDialog CreateDialog()
        {
            var dialog = new SaveFileDialog
            {
                DefaultExt = this._Filters.DefaultExtension,
                Title = this._Title,
                Filter = this._Filters.CreateFilterExpression(),
                InitialDirectory = this._InitialDirectory,
                OverwritePrompt = this._PromptForOverwrite,
                CreatePrompt = this._PromptForCreate,
                AddExtension = true,
                CheckPathExists = true,
            };
            return dialog;
        }

        /// <summary>
        /// Performs an action on the selected file before the result is completed
        /// </summary>
        /// <param name="action">The action to be performed</param>
        /// <returns></returns>
        public MySaveFileResult WithFileDo(Action<string> action)
        {
            this._FileAction = action;
            return this;
        }

        /// <summary>
        /// Create file filter for the dialog
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public MySaveFileResult FilterFiles(Action<FileFilterCollection> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "action may not be null");
            }
            action(this._Filters);
            return this;
        }

        /// <summary>
        /// Sets the inital <paramref name="directory" /> of the dialog
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public MySaveFileResult In(string directory)
        {
            if (Directory.Exists(directory) == false)
            {
                throw new ArgumentException(string.Format("Directory '{0}' doesn't exist", directory));
            }

            this._InitialDirectory = directory;
            return this;
        }

        /// <summary>
        /// Ask the user for permission if the file will be overriden
        /// </summary>
        /// <returns></returns>
        public MySaveFileResult PromptForOverwrite()
        {
            this._PromptForOverwrite = true;
            return this;
        }

        /// <summary>
        /// Ask the user for permission if a new file will be created
        /// </summary>
        /// <returns></returns>
        public MySaveFileResult PromptForCreate()
        {
            this._PromptForCreate = true;
            return this;
        }

        /// <summary>
        /// Dont cancel the execution of the coroutine if the user cancels the dialog
        /// </summary>
        /// <returns></returns>
        public MySaveFileResult IgnoreUserCancel()
        {
            this._IgnoreUserCancel = true;
            return this;
        }
    }
}
