namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    /// <summary>
    /// Base class for zoom related mouse handlers.
    /// </summary>
    public abstract class ZoomMouseHandler : MouseHandler
    {
        public static readonly float[] DEFAULT_ZOOM_STEPS = new float[] { 0.125f, 0.5f, 0.75f, 1.0f, 1.5f, 2.0f, 3.0f, 4.0f, 6.0f };
        private readonly IZoomableVCItemContainer layer;
        private readonly float[] zoomSteps;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected ZoomMouseHandler(IZoomableVCItemContainer layer, float[] zoomSteps, MouseHandler next)
            : base(next)
        {
            this.layer = layer;
            this.zoomSteps = zoomSteps;
        }

        /// <summary>
        /// Gets the container this handler controls
        /// </summary>
        protected IZoomableVCItemContainer ZoomLayer
        {
            get { return layer; }
        }

        /// <summary>
        /// Gets the next zoom step from the given factor.
        /// </summary>
        /// <param name="zoomFactor"></param>
        /// <returns></returns>
        protected float GetNextZoomStep(float zoomFactor)
        {
            for (int i = 0; i < zoomSteps.Length; i++)
            {
                if (zoomSteps[i] > zoomFactor)
                {
                    return zoomSteps[i];
                }
            }
            return zoomFactor;
        }

        /// <summary>
        /// Gets the previous zoom step from the given factor.
        /// </summary>
        /// <param name="zoomFactor"></param>
        /// <returns></returns>
        protected float GetPreviousZoomStep(float zoomFactor)
        {
            for (int i = zoomSteps.Length - 1; i >= 0; i--)
            {
                if (zoomSteps[i] < zoomFactor)
                {
                    return zoomSteps[i];
                }
            }
            return zoomFactor;
        }

        /// <summary>
        /// Gets all zoom steps.
        /// Do not modify the returned array.
        /// </summary>
        protected float[] ZoomSteps
        {
            get { return zoomSteps; }
        }
    }
}
