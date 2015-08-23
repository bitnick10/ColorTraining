namespace Phoenix
{
    public class Color { }
    public struct RGB96
    {
        public float r;  // r∈[0,1]
        public float g;  // g∈[0,1]
        public float b;  // b∈[0,1]
        public RGB96(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }
    public struct HSB96
    {
        private float h; // h∈[0, 360)
        private float s; // s∈[0, 1]
        private float b; // b∈[0, 1]
        public float H {
            get { return h; }
            set {h = value; }
        }
        public float S
        {
            get { return s; }
            set { s = value; }
        }
        public float B
        {
            get { return b; }
            set { b = value; }
        }
        public HSB96(float h, float s, float b)
        {
            this.h = h;
            this.s = s;
            this.b = b;
        }
        public RGB96 ToRGB96()
        {
            HSB96 hsb = this;
            int hi = (int)(hsb.h / 60) % 6;
            float f = hsb.h / 60 - hi;
            float p = hsb.b * (1 - hsb.s);
            float q = hsb.b * (1 - f * hsb.s);
            float t = hsb.b * (1 - (1 - f) * hsb.s);
            if (hi == 0)
            {
                RGB96 ret = new RGB96(hsb.b, t, p);
                return ret;
            }
            else if (hi == 1)
            {
                RGB96 ret = new RGB96(q, hsb.b, p);
                return ret;
            }
            else if (hi == 2)
            {
                RGB96 ret = new RGB96(p, hsb.b, t);
                return ret;
            }
            else if (hi == 3)
            {
                RGB96 ret = new RGB96(p, q, hsb.b);
                return ret;
            }
            else if (hi == 4)
            {
                RGB96 ret = new RGB96(t, p, hsb.b);
                return ret;
            }
            else
            {
                RGB96 ret = new RGB96(hsb.b, p, q);
                return ret;
            }
        }
    }
}
