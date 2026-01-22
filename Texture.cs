public partial class Texture {
	public enum Format {
		rgba8888 = 0,
		rgb888 = 1,
		g8 = 2,
		g32 = 3,
	}

	public Format Type;
	public uint Width;
	public uint Height;
	public uint Depth;

	private int GetChannelCount(Format f) {
		switch (f) {
			case Format.rgba8888:
				return 4;
			case Format.rgb888:
				return 3;
		}
		return 1;
	}
	protected void ReadHeader(BinaryReader r) {
		r.ReadBytes(4); //btex
		Type = (Format)r.ReadByte();
		Width = r.ReadUInt16();
		Height = r.ReadUInt16();
		Depth = r.ReadUInt16();
	}
	protected T[] ReadData<T>(BinaryReader r, int channels) {
		var d = new T[Width * Height * Depth * channels];
		var w = 0;
		var c = new object[GetChannelCount(Type)];
		for (var z = 0; z < Depth; z++) {
			for (var y = 0; y < Height; y++) {
				for (var x = 0; x < Width; x++) {
					for (var ch = 0; ch < c.Length; ch++)
						c[ch] = Type == Format.g32 ? r.ReadUInt32() : r.ReadByte();
					var run = r.ReadByte() + 1;
					for (var i = 0; i < run; i++) {
						for (var ch = 0; ch < channels; ch++)
							d[w++] = (T)(ch < c.Length ? c[ch] : (ch < 4 ? 0 : 1));
					}
				}
			}
		}
		return d;
	}
}