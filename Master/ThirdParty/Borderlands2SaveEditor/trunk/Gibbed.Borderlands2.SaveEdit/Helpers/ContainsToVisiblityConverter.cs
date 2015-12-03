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
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Gibbed.Borderlands2.SaveEdit
{
    internal class ContainsToVisiblityConverter : IMultiValueConverter
    {
        public object Convert(object[] values,
                              Type targetType,
                              object parameter,
                              System.Globalization.CultureInfo culture)
        {
            if (values == null ||
                values.Length != 2)
            {
                return Visibility.Collapsed;
            }

            if (values[0] is IEnumerable<string>)
            {
                var a = (IEnumerable<string>)values[0];
                var b = values[1];

                if (a.Contains(b) == true)
                {
                    return Visibility.Collapsed;
                }
            }
            else if (values[0] is ItemCollection)
            {
                var a = (ItemCollection)values[0];
                var b = values[1];

                if (a.Contains(b) == true)
                {
                    return Visibility.Collapsed;
                }
            }

            return Visibility.Visible;
        }

        public object[] ConvertBack(object value,
                                    Type[] targetTypes,
                                    object parameter,
                                    System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
