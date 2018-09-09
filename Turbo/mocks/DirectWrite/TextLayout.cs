using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDX.DirectWrite
{
	public class TextLayout : IDisposable
	{
		public Metrics Metrics { get; set; }

		public void Dispose() { }
	}

	public class Metrics
	{
		public float Height { get; set; }
		public float Width { get; set; }
	}
}
