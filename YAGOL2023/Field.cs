using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAGOL2023
{
    public class Field
    {
        private int[,] field1;
        private int[,] field2;

        public int Size
        {
            get { return field1.GetLength(0); }
            private set { SetSize(value); }
        }
        public void SetSize(int size)
        {
            field1 = new int[size, size];
            field2 = new int[size, size];

            field1.Initialize();
            field2.Initialize();
        }
        public int Get(int index1, int index2)
        {
            return field1[(Size+index1) % Size,(Size+index2) % Size];
        }
        public bool IsEmpty(int index1, int index2)
        {
            return Get(index1, index2) == 0;
        }
        public int this[int index1, int index2]
        {
            get
            { /* return the specified index here */
                return Get(index1, index2);
            }
            set
            { /* set the specified index to value here */
                field1[index1 % Size, index2 % Size] = value;
            }
        }
        public int Near(int index1, int index2)
        {
            var res = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (!IsEmpty(index1 + i, index2 + j))
                        res = res + 1;
                }
            }
            return res;
        }
        public int Around(int index1, int index2)
        {
            var res = Near(index1, index2);
            if (IsEmpty(index1, index2)) return res;
            return res - 1;
        }
        public void Rules(int index1, int index2)
        {
            if (IsEmpty(index1, index2) && Around(index1, index2) == 3)
            {
                field2[index1, index2] = 1;
                return;
            }
            if (!IsEmpty(index1, index2) && Around(index1, index2) > 3)
            {
                field2[index1, index2] = 0;
                return;
            }
            if (!IsEmpty(index1, index2) && Around(index1, index2) < 2)
            {
                field2[index1, index2] = 0;
                return;
            }
            field2[index1, index2] = field1[index1, index2];
        }
        public void Update()
        {
            field2.Initialize();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Rules(i, j);
                }
            }
            var tmp = field1;
            field1 = field2;
            field2 = tmp;
        }

        public void RandomFill(int treshold = 50)
        {
            field1.Initialize();
            Random random = new Random();
            for (int i = 0;i < Size;i++)
            {
                for(int j = 0;j < Size;j++)
                {
                    field1[i,j] = random.Next(100)>treshold ? 1 : 0;
                }
            }
        }
        public void SimmetryFill(int treshold = 50)
        {
            field1.Initialize();
            Random random = new Random();
            var s2 = Size / 2;
            for (int i = 0; i < s2; i++)
            {
                for (int j = 0; j < s2; j++)
                {
                    var v = random.Next(100) > treshold ? 1 : 0;
                    field1[i, j] = v;
                    field1[Size-i-1, j] = v;
                    field1[i, Size - j-1] = v;
                    field1[Size - i - 1, Size - j - 1] = v;

                }
            }
        }

        public void Paint(System.Drawing.Graphics g,
            System.Drawing.Pen pen = null)
        {
            if (pen == null) pen = Pens.CadetBlue;
            float sx= g.VisibleClipBounds.Width/Size;
            float sy = g.VisibleClipBounds.Height/Size;
            float r = sx/2.1f;

            for (int i = 0; i<Size; i++)
            {
                for( int j = 0; j < Size;j++)
                {
                    if (!IsEmpty(i, j))
                    {
                        float x = (int)(i * sx);
                        float y = (int)(j * sy);
                        //g.DrawLine(pen, x - r, y - r, x + r, y + r);
                        // g.DrawLine(pen, x - r, y + r, x + r, y - r);

                        g.DrawEllipse(pen, x - r, y - r, 2*r, 2* r);
                        g.DrawEllipse(pen, x - r/2, y - r/2, 2 * r/2, 2 * r/2);
                    }
                }
            }

        }
    }
}
