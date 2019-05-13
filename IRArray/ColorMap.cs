using System;

namespace IRArray
{
    public class ColorMap
    {
        private int colormapLength = 256;
        private int alphaValue = 255;

        public ColorMap()
        {
        }

        public ColorMap(int colorLength)
        {
            colormapLength = colorLength;
        }

        public ColorMap(int colorLength, int alpha)
        {
            colormapLength = colorLength;
            alphaValue = alpha;
        }

        public int[,] Spring()
        {
            int[,] cmap = new int[colormapLength, 4];
            //float[] spring = new float[colormapLength];
            for (int i = 0; i < colormapLength; i++)
            {
                //spring[i] = 1.0f * i / (colormapLength - 1);
                //cmap[i, 0] = alphaValue;
                //cmap[i, 1] = 255;
                //cmap[i, 2] = (int)(255 * spring[i]);
                //cmap[i, 3] = 255 - cmap[i, 1];

                float spring = 1.0f * i / (colormapLength - 1);
                cmap[i, 0] = alphaValue;
                cmap[i, 1] = 255;
                cmap[i, 2] = (int)(255 * spring);
                cmap[i, 3] = 0;
            }
            return cmap;
        }

        public int[,] Summer()
        {
            int[,] cmap = new int[colormapLength, 4];
            //float[] summer = new float[colormapLength];
            for (int i = 0; i < colormapLength; i++)
            {
                //summer[i] = 1.0f * i / (colormapLength - 1);
                //cmap[i, 0] = alphaValue;
                //cmap[i, 1] = (int)(255 * summer[i]);
                //cmap[i, 2] = (int)(255 * 0.5f * (1 + summer[i]));
                //cmap[i, 3] = (int)(255 * 0.4f);

                float summer = 1.0f * i / (colormapLength - 1);
                cmap[i, 0] = alphaValue;
                cmap[i, 1] = (int)(255 * summer);
                cmap[i, 2] = (int)(255 * 0.5f * (1 + summer));
                cmap[i, 3] = (int)(255 * 0.4f);
            }
            return cmap;
        }

        public int[,] Autumn()
        {
            int[,] cmap = new int[colormapLength, 4];
            //float[] autumn = new float[colormapLength];
            for (int i = 0; i < colormapLength; i++)
            {
                //autumn[i] = 1.0f * i / (colormapLength - 1);
                //cmap[i, 0] = alphaValue;
                //cmap[i, 1] = 255;
                //cmap[i, 2] = (int)(255 * autumn[i]);
                //cmap[i, 3] = 0;

                float autumn = 1.0f * i / (colormapLength - 1);
                cmap[i, 0] = alphaValue;
                cmap[i, 1] = 255;
                cmap[i, 2] = (int)(255 * autumn);
                cmap[i, 3] = 0;
            }
            return cmap;
        }

        public int[,] Winter()
        {
            int[,] cmap = new int[colormapLength, 4];
            //float[] winter = new float[colormapLength];
            for (int i = 0; i < colormapLength; i++)
            {
                //winter[i] = 1.0f * i / (colormapLength - 1);
                //cmap[i, 0] = alphaValue;
                //cmap[i, 1] = 0;
                //cmap[i, 2] = (int)(255 * winter[i]);
                //cmap[i, 3] = (int)(255 * (1.0f - 0.5f * winter[i]));

                float winter = 1.0f * i / (colormapLength - 1);
                cmap[i, 0] = alphaValue;
                cmap[i, 1] = 0;
                cmap[i, 2] = (int)(255 * winter);
                cmap[i, 3] = (int)(255 * (1.0f - 0.5f * winter));
            }
            return cmap;
        }

        public int[,] Gray()
        {
            int[,] cmap = new int[colormapLength, 4];
            //float[] gray = new float[colormapLength];
            for (int i = 0; i < colormapLength; i++)
            {
                //gray[i] = 1.0f * i / (colormapLength - 1);
                //cmap[i, 0] = alphaValue;
                //cmap[i, 1] = (int)(255 * gray[i]);
                //cmap[i, 2] = (int)(255 * gray[i]);
                //cmap[i, 3] = (int)(255 * gray[i]);
                //gray[i] = 1.0f * i / (colormapLength - 1);
                float gray = 1.0f * i / (colormapLength - 1);
                cmap[i, 0] = alphaValue;
                cmap[i, 1] = (int)(255 * gray);
                cmap[i, 2] = (int)(255 * gray);
                cmap[i, 3] = (int)(255 * gray);
            }
            return cmap;
        }

        public int[,] Jet()
        {
            int[,] cmap = new int[colormapLength, 4];
            float[,] cMatrix = new float[colormapLength, 3];
            int n = (int)Math.Ceiling(colormapLength / 4.0f);
            int nMod = 0;
            float[] fArray = new float[3 * n - 1];
            int[] red = new int[fArray.Length];
            int[] green = new int[fArray.Length];
            int[] blue = new int[fArray.Length];

            if (colormapLength % 4 == 1)
            {
                nMod = 1;
            }

            for (int i = 0; i < fArray.Length; i++)
            {
                if (i < n)
                    fArray[i] = (float)(i + 1) / n;
                else if (i >= n && i < 2 * n - 1)
                    fArray[i] = 1.0f;
                else if (i >= 2 * n - 1)
                    fArray[i] = (float)(3 * n - 1 - i) / n;
                green[i] = (int)Math.Ceiling(n / 2.0f) - nMod + i;
                red[i] = green[i] + n;
                blue[i] = green[i] - n;
            }

            int nb = 0;
            for (int i = 0; i < blue.Length; i++)
            {
                if (blue[i] > 0)
                    nb++;
            }

            for (int i = 0; i < colormapLength; i++)
            {
                for (int j = 0; j < red.Length; j++)
                {
                    if (i == red[j] && red[j] < colormapLength)
                    {
                        cMatrix[i, 0] = fArray[i - red[0]];
                    }
                }
                for (int j = 0; j < green.Length; j++)
                {
                    if (i == green[j] && green[j] < colormapLength)
                        cMatrix[i, 1] = fArray[i - (int)green[0]];
                }
                for (int j = 0; j < blue.Length; j++)
                {
                    if (i == blue[j] && blue[j] >= 0)
                        cMatrix[i, 2] = fArray[fArray.Length - 1 - nb + i];
                }
            }

            for (int i = 0; i < colormapLength; i++)
            {
                cmap[i, 0] = alphaValue;
                for (int j = 0; j < 3; j++)
                {
                    cmap[i, j + 1] = (int)(cMatrix[i, j] * 255);
                }
            }
            return cmap;
        }

        public int[,] Hot()
        {
            int[,] cmap = new int[colormapLength, 4];
            int n = 3 * colormapLength / 8;
            //float[] red = new float[colormapLength];
            //float[] green = new float[colormapLength];
            //float[] blue = new float[colormapLength];
            for (int i = 0; i < colormapLength; i++)
            {
                //if (i < n)
                //    red[i] = 1.0f * (i + 1) / n;
                //else
                //    red[i] = 1.0f;
                //if (i < n)
                //    green[i] = 0f;
                //else if (i >= n && i < 2 * n)
                //    green[i] = 1.0f * (i + 1 - n) / n;
                //else
                //    green[i] = 1f;
                //if (i < 2 * n)
                //    blue[i] = 0f;
                //else
                //    blue[i] = 1.0f * (i + 1 - 2 * n) / (colormapLength - 2 * n);
                //cmap[i, 0] = alphaValue;
                //cmap[i, 1] = (int)(255 * red[i]);
                //cmap[i, 2] = (int)(255 * green[i]);
                //cmap[i, 3] = (int)(255 * blue[i]);

                float red;
                float green;
                float blue;
                if (i < n) { red = 1.0f * (i + 1) / n; }
                else { red = 1.0f; }

                if (i < n) { green = 0f; }
                else if (i >= n && i < 2 * n) { green = 1.0f * (i + 1 - n) / n; }
                else { green = 1f; }

                if (i < 2 * n) { blue = 0f; }
                else { blue = 1.0f * (i + 1 - 2 * n) / (colormapLength - 2 * n); }
                cmap[i, 0] = alphaValue;
                cmap[i, 1] = (int)(255 * red);
                cmap[i, 2] = (int)(255 * green);
                cmap[i, 3] = (int)(255 * blue);
            }
            return cmap;
        }

        public int[,] Cool()
        {
            int[,] cmap = new int[colormapLength, 4];
            //float[] cool = new float[colormapLength];
            for (int i = 0; i < colormapLength; i++)
            {
                //cool[i] = 1.0f * i / (colormapLength - 1);
                //cmap[i, 0] = alphaValue;
                //cmap[i, 1] = (int)(255 * cool[i]);
                //cmap[i, 2] = (int)(255 * (1 - cool[i]));
                //cmap[i, 3] = 255;

                float cool = 1.0f * i / (colormapLength - 1);
                cmap[i, 0] = alphaValue;
                cmap[i, 1] = (int)(255 * cool);
                cmap[i, 2] = (int)(255 * (1 - cool));
                cmap[i, 3] = 255;
            }
            return cmap;
        }

        public int[,] CMRmap()
        {
            int[,] cmap = new int[colormapLength, 4];
            int n = (int)Math.Ceiling(colormapLength / 8.0f);
            //float[] cmr = new float[colormapLength];
            //int[] red = new int[colormapLength];
            //int[] green = new int[colormapLength];
            //int[] blue = new int[colormapLength];
            for (int i = 0; i < colormapLength; i++)
            {
                float red = 0;
                float green = 0;
                float blue = 0;
                //if (i < n) { red = green = blue = 0; }
                //else if (i < 2 * n) { red = 0.15f; green = 0.15f; blue = 0.5f; }
                //else if (i < 3 * n) { red = 0.3f; green = 0.15f; blue = 0.75f; }
                //else if (i < 4 * n) { red = 0.6f; green = 0.2f; blue = 0.5f; }
                //else if (i < 5 * n) { red = 1.0f; green = 0.25f; blue = 0.15f; }
                //else if (i < 6 * n) { red = 0.9f; green = 0.5f; blue = 0; }
                //else if (i < 7 * n) { red = 0.9f; green = 0.75f; blue = 0.1f; }
                //else if (i < 8 * n) { red = 0.9f; green = 0.9f; blue = 0.5f; }
                //else if (i < 9 * n) { red = 1.0f; green = 1.0f; blue = 1.0f; }

                //float red = 0;
                //float green = 0;
                //float blue = 0;
                float cmr = 0;
                if (i <= n)
                {
                    cmr = 1.0f * i / n;
                    red = green = cmr * 0.15f;
                    blue = cmr * 0.5f;
                }
                else if (i <= 2 * n)
                {
                    cmr = 1.0f * (i - n) / n;
                    red = cmr * 0.15f + 0.15f;
                    green = 0.15f;
                    blue = cmr * 0.25f + 0.5f;
                }
                else if (i <= 3 * n)
                {
                    cmr = 1.0f * (i - 2 * n) / n;
                    red = cmr * 0.3f + 0.3f;
                    green = cmr * 0.05f + 0.15f;
                    blue = cmr * -0.25f + 0.75f;
                }
                else if (i <= 4 * n)
                {
                    cmr = 1.0f * (i - 3 * n) / n;
                    red = cmr * 0.4f + 0.6f;
                    green = cmr * 0.05f + 0.2f;
                    blue = cmr * -0.35f + 0.5f;
                }
                else if (i <= 5 * n)
                {
                    cmr = 1.0f * (i - 4 * n) / n;
                    red = cmr * -0.1f + 1f;
                    green = cmr * 0.25f + 0.25f;
                    blue = cmr * -0.15f + 0.15f;
                }
                else if (i <= 6 * n)
                {
                    cmr = 1.0f * (i - 5 * n) / n;
                    red = 0.9f;
                    green = cmr * 0.25f + 0.5f;
                    blue = cmr * 0.1f;
                }
                else if (i <= 7 * n)
                {
                    cmr = 1.0f * (i - 6 * n) / n;
                    red = 0.9f;
                    green = cmr * 0.15f + 0.75f;
                    blue = cmr * 0.4f + 0.1f;
                }
                else if (i <= 8 * n)
                {
                    cmr = 1.0f * (i - 7 * n) / n;
                    red = green = cmr * 0.1f + 0.9f;
                    blue = cmr * 0.5f + 0.5f;
                }

                cmap[i, 0] = alphaValue;
                cmap[i, 1] = (int)(255 * red);
                cmap[i, 2] = (int)(255 * green);
                cmap[i, 3] = (int)(255 * blue);
            }
            return cmap;
        }
    }
}
