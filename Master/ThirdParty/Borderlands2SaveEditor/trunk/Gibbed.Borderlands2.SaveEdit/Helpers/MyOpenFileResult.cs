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
using System.Linq;
using Caliburn.Micro;
using Caliburn.Micro.Contrib.Helper;
using Microsoft.Win32;

namespace Gibbed.Borderlands2.SaveEdit
{
    // The original OpenFileResult continues execution in FileOk, which is bad
    // since that can execute before the dialog has closed.

    /// <summary>
    /// A Caliburn.Micro result which shows an OpenFileDialog to user, allowing him to select one (default) or more files.
    /// You can also directly perform an action on the selected file(s) inside the result.
    /// </summary>
    public class MyOpenFileResult : IResult
    {
        private readonly FileFilterCollection _Filters = new FileFilterCollection();
        private readonly string _Title;
        private bool _AllowMultipleFiles;
        private EventHandler<ResultCompletionEventArgs> _Completed = delegate { };
        private Action<string[]> _FileAction;
        private bool _IgnoreUserCancel;
        private string _InitialDirectory;

        /// <summary>
        /// Creates a new MyOpenFileResult
        /// </summary>
        /// <param name="title">The title of the dialog</param>
        public MyOpenFileResult(string title = null)
        {
            this._Title = title;
        }

        /// <summary>
        /// The name of the file selected by the user. If the user selected multiple
        /// files, this property will return the first name in the FileNames array
        /// </summary>
        public string FileName
        {
            get { return this.FileNames.Any() ? this.FileNames[0] : null; }
        }

        /// <summary>
        /// The name of all files the user selected.
        /// </summary>
        public string[] FileNames { get; private set; }

        #region IResult Members
        void IResult.Execute(ActionExecutionContext context)
        {
            var dialog = CreateDialog();

            if (dialog.ShowDialog() != true)
            {
                this.OnCompleted(new ResultCompletionEventArgs
                {
                    WasCancelled = this._IgnoreUserCancel == false
                });
                return;
            }

            var resultArgs = new ResultCompletionEventArgs();

            this.FileNames = dialog.FileNames;

            if (this._FileAction != null)
            {
                try
                {
                    this._FileAction(FileNames);
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
        protected virtual OpenFileDialog CreateDialog()
        {
            var dialog = new OpenFileDialog
            {
                Title = this._Title,
                Filter = this._Filters.CreateFilterExpression(),
                /////////////////////////////////////////////////
                //Spitfire mods
                //InitialDirectory = this._InitialDirectory,
                //End Spitfire mods
                /////////////////////////////////////////////////
                Multiselect = this._AllowMultipleFiles,
                CheckFileExists = true,
                CheckPathExists = true,
            };
            return dialog;
        }

        /// <summary>
        /// Performs an action on the selected file before the result is completed
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public MyOpenFileResult WithFileDo(Action<string> action)
        {
            this._FileAction = files => action(files[0]);
            return this;
        }

        /// <summary>
        /// Performs an action on the selected files before the result is completed
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public MyOpenFileResult WithFilesDo(Action<string[]> action)
        {
            this._FileAction = action;
            return this;
        }

        /// <summary>
        /// Create file filter for the dialog
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public MyOpenFileResult FilterFiles(Action<FileFilterCollection> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "action may not be null");
            }
            action(this._Filters);
            return this;
        }

        /// <summary>
        /// Sets the initial <paramref name="directory" /> of the dialog
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public MyOpenFileResult In(string directory)
        {
            if (!Directory.Exists(directory))
            {
                throw new ArgumentException(string.Format("Directory '{0}' doesn't exist", directory));
            }

            this._InitialDirectory = directory;
            return this;
        }

        /// <summary>
        /// Allow the user to select multiple files
        /// </summary>
        /// <returns></returns>
        public MyOpenFileResult AllowMultipleFiles()
        {
            this._AllowMultipleFiles = true;
            return this;
        }

        /// <summary>
        /// Dont cancel the execution of the coroutine if the user cancels the dialog
        /// </summary>
        /// <returns></returns>
        public MyOpenFileResult IgnoreUserCancel()
        {
            this._IgnoreUserCancel = true;
            return this;
        }
    }
}
