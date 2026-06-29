namespace btex;

[Convert.Association(["tga"])]
public class Targa : Convert.Source {
	private byte[] Data;

	public override void Read(string path) {
		var tga = new TargaSharp.Targa(path);
		Width = tga.Width;
		Height = tga.Height;
		Depth = 1;
		Data = new byte[Width * Height * Depth * 4];
		var w = 0;
		var bmp = tga.ToBitmap(true);
		for (var y = 0; y < Height; y++) {
			for (var x = 0; x < Width; x++) {
				var c = bmp.GetPixel(x, Width - y - 1);
				if (x == 0 && y == 0)
					Console.WriteLine(c.R);
				Data[w++] = c.R;
				Data[w++] = c.G;
				Data[w++] = c.B;
				Data[w++] = c.A;
			}
		}
	}
	public override byte[] LoadByte(int channels) {
		var load = new byte[Width * Height * channels];
		var loadw = 0;
		var ch = 0;
		foreach (var pixel in Data) {
			if (ch < channels)
				load[loadw++] = pixel;
			ch++;
			if (ch > 3)
				ch = 0;
		}
		return load;
	}
	public override uint[] LoadUint(int channels) {
		throw new("uint tga unsupported");
	}
}