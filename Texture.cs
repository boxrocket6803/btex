public partial class Texture {
	public enum Format {
		rgba8888 = 0,
		rgb888 = 1,
		g8 = 2,
		g32 = 3,
	}

	public uint Width;
	public uint Height;
	public uint Depth;
}