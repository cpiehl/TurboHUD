using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDX
{
	public class RectangleF
	{
		public float Height { get; set; }
		public float Width { get; set; }
		public float X { get; set; }
		public float Y { get; set; }

		public Point Center { get; set; }
		public float Bottom { get; set; }
		public float Right { get; set; }
		public float Top { get; set; }
		public float Left { get; set; }

		public RectangleF(float X, float Y, float W, float H) { }
	}

	public class Point
	{
		public float X { get; set; }
		public float Y { get; set; }
	}
}
