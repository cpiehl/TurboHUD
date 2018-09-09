
namespace Turbo.Plugins.Chuzuki
{
	public class Color
	{
		public int A { get; set; }
		public int R { get; set; }
		public int G { get; set; }
		public int B { get; set; }

		public Color(int R, int G, int B) : this(255, R, G, B) { }

		public Color(int A, int R, int G, int B)
		{
			this.A = A;
			this.R = R;
			this.G = G;
			this.B = B;
		}
	}
}
