namespace btex;

[Convert.Association(["tga"])]
public class Targa : Convert.Source {
	private float[] Data;

	public override void Read(string path) {
		var tga = new TargaSharp.Targa(path);
		Width = tga.Width;
		Height = tga.Height;
		Depth = 1;
		Data = new float[Width * Height * Depth * 4];
		var w = 0;
		var bmp = tga.ToBitmap(true);
		for (var x = 0; x < Width; x++) {
			for (var y = 0; y < Height; y++) {
				var c = bmp.GetPixel(x, y);
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
				load[loadw++] = (byte)(pixel * byte.MaxValue);
			ch++;
			if (ch > 3)
				ch = 0;
		}
		return load;
	}
	public override uint[] LoadUint(int channels) {
		var load = new uint[Width * Height * channels];
		var loadw = 0;
		var ch = 0;
		foreach (var pixel in Data) {
			if (ch < channels)
				load[loadw++] = (uint)(pixel * uint.MaxValue);
			ch++;
			if (ch > 3)
				ch = 0;
		}
		return load;
	}
}