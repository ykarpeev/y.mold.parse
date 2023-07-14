using System.Reflection;

namespace Y.Mold.Parse.Services
{
    /// <summary>
    /// A molding tag instance.
    /// </summary>
    public class MoldingTag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoldingTag"/> class.
        /// </summary>
        /// <param name="id">Tag ID.</param>
        /// <param name="x1">Min X.</param>
        /// <param name="y1">Min Y.</param>
        /// <param name="x2">Max X.</param>
        /// <param name="y2">Max Y.</param>
        /// <param name="text">Object text.</param>
        /// <param name="isText">Whether this is a text object.</param>
        /// <param name="stroke">Stroke string.</param>
        /// <param name="height_start">Starting height.</param>
        /// <param name="height_end">Ending height.</param>
        /// <param name="height_avg">Average height.</param>
        /// <param name="vectorPath">Vector path.</param>
        public MoldingTag(string id, double x1, double y1, double x2, double y2, string text, bool isText, string stroke, double height_start, double height_end, double height_avg, string vectorPath)
        {
            this.ID = id;
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
            this.Text = text;
            this.IsText = isText;
            this.Stroke = stroke;
            this.Height_Start = height_start;
            this.Height_End = height_end;
            this.Height_Avg = height_avg;
            this.VectorPath = vectorPath;
        }

        /// <summary>
        /// Gets the ID.
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// Gets the Min X.
        /// </summary>
        public double X1 { get; private set; }

        /// <summary>
        /// Gets the Min Y.
        /// </summary>
        public double Y1 { get; private set; }

        /// <summary>
        /// Gets the Max X.
        /// </summary>
        public double X2 { get; private set; }

        /// <summary>
        /// Gets the Max Y.
        /// </summary>
        public double Y2 { get; private set; }

        /// <summary>
        /// Gets the text data.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Gets a value indicating whether or not this is a text object.
        /// </summary>
        public bool IsText { get; private set; }

        /// <summary>
        /// Gets the stroke.
        /// </summary>
        public string Stroke { get; private set; }

        /// <summary>
        /// Gets the height start.
        /// </summary>
        public double Height_Start { get; private set; }

        /// <summary>
        /// Gets the height end.
        /// </summary>
        public double Height_End { get; private set; }

        /// <summary>
        /// Gets the height average.
        /// </summary>
        public double Height_Avg { get; private set; }

        /// <summary>
        /// Gets the object width.
        /// </summary>
        public double Width => Math.Abs(this.X2 - this.X1);

        /// <summary>
        /// Gets the object height.
        /// </summary>
        public double Height => Math.Abs(this.Y2 - this.Y1);

        /// <summary>
        /// Gets the actual width (if height is larger than width then it is the width).
        /// </summary>
        public double ActualWidth
        {
            get
            {
                if (this.Width > this.Height)
                {
                    return this.Width;
                }

                return this.Height;
            }
        }

        /// <summary>
        /// Gets the actual height (or width if it is wider).
        /// </summary>
        public double ActualHeight
        {
            get
            {
                if (this.Height > this.Width)
                {
                    return this.Height;
                }

                return this.Width;
            }
        }

        /// <summary>
        /// Gets the center X position.
        /// </summary>
        public double CenterX
        {
            get
            {
                return (this.X1 + this.X2) / 2;
            }
        }

        /// <summary>
        /// Gets the center Y position.
        /// </summary>
        public double CenterY
        {
            get
            {
                return (this.Y1 + this.Y2) / 2;
            }
        }

        /// <summary>
        /// Gets the angle of the tag object.
        /// </summary>
        public double Angle
        {
            get
            {
                var radian = Math.Atan2(this.Y1 - this.Y2, this.X1 - this.X2);
                var angle = ((radian * (180 / Math.PI)) + 360) % 360;

                return angle;
            }
        }

        /// <summary>
        /// Gets the vector path.
        /// </summary>
        public string VectorPath { get; }

        /// <summary>
        /// Gets a value indicating whether or not this is a vector object.
        /// </summary>
        public bool IsVector
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.VectorPath);
            }
        }

        /// <inheritdoc/>
        public override bool Equals(object other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != other.GetType())
            {
                return false;
            }

            foreach (PropertyInfo p in this.GetType().GetProperties())
            {
                if (!object.Equals(p.GetValue(other, null), p.GetValue(this, null)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}