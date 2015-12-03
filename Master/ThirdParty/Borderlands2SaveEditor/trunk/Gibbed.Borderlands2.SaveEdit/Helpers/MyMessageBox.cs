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
using System.Windows;
using Caliburn.Micro;

namespace Gibbed.Borderlands2.SaveEdit
{
    public class MyMessageBox : IResult
    {
        private readonly string _Text;
        private readonly string _Caption;
        private MessageBoxButton _Button = MessageBoxButton.OK;
        private MessageBoxImage _Icon = MessageBoxImage.None;
        private MessageBoxResult _DefaultResult = MessageBoxResult.OK;
        private MessageBoxOptions _Options = MessageBoxOptions.None;
        private EventHandler<ResultCompletionEventArgs> _Completed = delegate { };
        private Action<MessageBoxResult> _ResultAction;

        public MyMessageBox(string text = null, string caption = null)
        {
            this._Text = text;
            this._Caption = caption;
        }

        public MessageBoxResult Result { get; private set; }

        #region IResult Members
        void IResult.Execute(ActionExecutionContext context)
        {
            var result = MessageBox.Show(this._Text, this._Caption, this._Button, this._Icon, this._DefaultResult, this._Options);

            var resultArgs = new ResultCompletionEventArgs();

            this.Result = result;

            if (this._ResultAction != null)
            {
                try
                {
                    this._ResultAction(result);
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

        public MyMessageBox WithButton(MessageBoxButton button)
        {
            this._Button = button;
            return this;
        }

        public MyMessageBox WithIcon(MessageBoxImage icon)
        {
            this._Icon = icon;
            return this;
        }

        public MyMessageBox WithDefaultResult(MessageBoxResult defaultResult)
        {
            this._DefaultResult = defaultResult;
            return this;
        }

        public MyMessageBox WithOptions(MessageBoxOptions options)
        {
            this._Options = options;
            return this;
        }

        public MyMessageBox WithResultDo(Action<MessageBoxResult> action)
        {
            this._ResultAction = action;
            return this;
        }
    }
}
