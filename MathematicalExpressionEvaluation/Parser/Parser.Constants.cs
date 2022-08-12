using System;
using System.Collections.Generic;

namespace JSribar.MathematicalExpressionEvaluation
{
	public partial class Parser
	{
		/// <summary>
		///   Mapping of string token to mathematical constant values.
		/// </summary>
		private readonly Dictionary<string, double> mathematicalConstantsMap = new Dictionary<string, double>()
		{
			{ "PI", Math.PI },
			{ "E", Math.E  },
		};
	}
}
