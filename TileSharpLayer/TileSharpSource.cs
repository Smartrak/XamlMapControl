using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using MapControl;
using TileSharp;

namespace TileSharpLayer
{
	public class TileSharpSource : ImageTileSource
	{
		private readonly ILabelOverlapPreventer _overlapPreventer;
		private readonly IFeatureCache _featureCache;
		private readonly LayerConfig _layerConfig;

		public TileSharpSource(ILabelOverlapPreventer overlapPreventer, IFeatureCache featureCache, LayerConfig layerConfig)
		{
			IsAsync = true;

			_overlapPreventer = overlapPreventer;
			_featureCache = featureCache;
			_layerConfig = layerConfig;
		}

		public override ImageSource LoadImage(int x, int y, int zoomLevel, int rotation)
		{
			using (var res = new TileSharp.GdiRenderer.Renderer(_overlapPreventer, _featureCache).GenerateTile(new TileConfig(zoomLevel, x, y, _layerConfig, rotation)))
			{
				var image = ImageFromBitmap(res);
				image.Freeze();
				return image;
			}
		}

		private ImageSource ImageFromBitmap(Bitmap bmp)
		{
			IntPtr hBitmap = bmp.GetHbitmap();

			try
			{
				return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
			}
			finally
			{
				DeleteObject(hBitmap);
			}
		}

		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr hObject);
	}
}
