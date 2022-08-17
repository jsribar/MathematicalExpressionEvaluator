/******************************************************************************
Copyright(c) 2022, Julijan Šribar

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
******************************************************************************/

using System;
using System.Collections.Generic;

namespace JSribar.MathematicalExpressionEvaluator
{
	public partial class Parser
	{
		/// <summary>
		///   Adds custom mathematical constant.
		/// </summary>
		/// <param name="name">
		///   Identifier of the constant.
		/// </param>
		/// <param name="value">
		///   Value of the constant.
		/// </param>
		public void AddConstant(string name, double value)
		{
            CheckIdentifier(name);
            mathematicalConstantsMap.Add(name, value);
        }

		/// <summary>
		///   Mapping of string token to mathematical constant values.
		/// </summary>
		private readonly Dictionary<string, double> mathematicalConstantsMap = new Dictionary<string, double>()
		{
			{ "PI", Math.PI },
			{ "π", Math.PI },
			{ "E", Math.E  },
		};
	}
}
