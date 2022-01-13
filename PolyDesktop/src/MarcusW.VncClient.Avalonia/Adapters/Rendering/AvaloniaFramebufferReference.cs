using System;
using Avalonia.Platform;
using MarcusW.VncClient.Rendering;

namespace MarcusW.VncClient.Avalonia.Adapters.Rendering
{
    /// <inheritdoc />
    public sealed class AvaloniaFramebufferReference : IFramebufferReference
    {
        private ILockedFramebuffer? _lockedFramebuffer;
        private readonly Action _invalidateVisual;

        /// <inheritdoc />
        public IntPtr Address => _lockedFramebuffer?.Address ?? throw new ObjectDisposedException(nameof(AvaloniaFramebufferReference));

        /// <inheritdoc />
        public Size Size => Conversions.GetSize(_lockedFramebuffer?.Size ?? throw new ObjectDisposedException(nameof(AvaloniaFramebufferReference)));

        /// <inheritdoc />
        public PixelFormat Format => Conversions.GetPixelFormat(_lockedFramebuffer?.Format ?? throw new ObjectDisposedException(nameof(AvaloniaFramebufferReference)));

        /// <inheritdoc />
        public double HorizontalDpi => _lockedFramebuffer?.Dpi.X ?? throw new ObjectDisposedException(nameof(AvaloniaFramebufferReference));

        /// <inheritdoc />
        public double VerticalDpi => _lockedFramebuffer?.Dpi.Y ?? throw new ObjectDisposedException(nameof(AvaloniaFramebufferReference));

        internal AvaloniaFramebufferReference(ILockedFramebuffer lockedFramebuffer, Action invalidateVisual)
        {
            _lockedFramebuffer = lockedFramebuffer;
            _invalidateVisual = invalidateVisual;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            ILockedFramebuffer? lockedFramebuffer = _lockedFramebuffer;
            _lockedFramebuffer = null;

            if (lockedFramebuffer == null)
                return;

            lockedFramebuffer.Dispose();

            // Dispose gets called, when rendering is finished, so invalidate the visual now
            _invalidateVisual();
        }
    }
}
