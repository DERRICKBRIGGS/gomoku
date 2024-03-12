using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gomoku
{
    class WhitePiece:piece
    {
        public WhitePiece(int x, int y):base(x, y) 
        {
            this.Image = Properties.Resources.white;
        }
        public override ColorType GetColor() 
        {
            return ColorType.White;
        }
    }
}
