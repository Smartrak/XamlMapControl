﻿// XAML Map Control - http://xamlmapcontrol.codeplex.com/
// Copyright © 2014 Clemens Fischer
// Licensed under the Microsoft Public License (Ms-PL)

#if WINDOWS_RUNTIME
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
#else
using System.Windows.Media;
using System.Windows.Media.Imaging;
#endif

namespace MapControl
{
    /// <summary>
    /// Provides the image of a map tile. ImageTileSource bypasses image
    /// downloading  in TileImageLoader. By overriding the LoadImage method,
    /// an application can provide tile images from an arbitrary source.
    /// </summary>
    public class ImageTileSource : TileSource
    {
        public virtual ImageSource LoadImage(int x, int y, int zoomLevel, int rotation)
        {
            var uri = GetUri(x, y, zoomLevel, rotation);

            return uri != null ? new BitmapImage(uri) : null;
        }
    }
}
