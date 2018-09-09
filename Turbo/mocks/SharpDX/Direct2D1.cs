using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDX.Direct2D1
{
	public enum DashStyle { Dash, Solid }
	public enum CapStyle { Flat }
	public enum FigureBegin { Filled }
	public enum FigureEnd { Closed }

	public class StrokeStyle
	{
		public DashStyle DashStyle { get; set; }
	}

	public class Geometry : IDisposable
	{
		public void AddLine(Vector2 v) { }
		public void BeginFigure(Point p, FigureBegin fb) { }
		public void BeginFigure(Vector2 v, FigureBegin fb) { }
		public void EndFigure(FigureEnd fe) { }
		public void Close() { }

		public void Dispose() { }
	}

	public class Vector2
	{
		public Vector2(float X, float Y) { }
	}
}
