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
