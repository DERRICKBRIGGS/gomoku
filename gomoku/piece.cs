using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gomoku
{
    abstract class piece : PictureBox
    {
        public static readonly int LEN_AND_WIDRH = 50;
        public piece(int x, int y) 
        {
            this.BackColor = Color.Transparent;
            //位子修正回歸點擊中心
            this.Location= new Point(x- LEN_AND_WIDRH/2, y- LEN_AND_WIDRH/2);
            this.Size = new Size(LEN_AND_WIDRH, LEN_AND_WIDRH);
        }
        //運用基礎class來生成抽象方法，然後透過繼承的子類別給予意義
        public abstract ColorType GetColor();
    }
}
