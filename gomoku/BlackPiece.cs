using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gomoku
{
    class BlackPiece : piece
    {
        public BlackPiece(int x, int y):base(x, y) 
        {
            this.Image=Properties.Resources.black;
        }
        public override ColorType GetColor() 
        {
            return ColorType.Black;
        }
    }
}
