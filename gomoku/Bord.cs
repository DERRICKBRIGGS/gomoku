using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace gomoku
{
    internal class Bord
    {
        private static readonly Point NO_MATCH_NODE = new Point(-1, -1);
        public static readonly int DISTANCE = 75; //棋盤交界處相隔距離
        public static readonly int PIECERANGE = 10;//旗子判定距離
        public static readonly int BOUNDARY = 75; //棋盤邊界距離
        public static readonly int NODE_COUNT = 9; //確認最大棋盤邊界
        private static piece[,] PieceArray = new piece[NODE_COUNT, NODE_COUNT];
        private Point lastPlaceNode = NO_MATCH_NODE; //紀錄現在下棋的位置(判斷勝利條件)
        private Point lastBlackNode = NO_MATCH_NODE;
        public Point LastPlaceNode { get { return lastPlaceNode; } }
        public Point LastBlackNode { get { return lastBlackNode; } }


        public bool pieceTure(int x, int y) 
        {
            //回傳座標是否在節點上(bool)
            //功能:確認是否在節點上，讓移標變成手暗示點擊，否則成為指標
            Point node = pieceClose(x, y);
            if (node == NO_MATCH_NODE)
                return false;
            else 
                return true;
        }
        //同上做結合，回傳座標是否在節點上，可以的話生成棋子，不行則回傳null
        //功能:確認是否能放棋子，可以則直接照指定座標生成
        public piece pieceCando(int x, int y, ColorType type) 
        {
            //換算x,y成棋盤座標
            Point node = pieceClose(x, y);
            if (node == NO_MATCH_NODE)
                return null;
            //確認位子是否有下過棋子，#備註原先陣列的储存都是null
            else if (PieceArray[node.X, node.Y] != null)
                return null;
            else 
            {
                Point FixPosition = pieceForm(node);
                if (type == ColorType.Black) 
                {
                    PieceArray[node.X, node.Y] = new BlackPiece(FixPosition.X, FixPosition.Y);
                    this.lastBlackNode = node;
                }
                else
                    PieceArray[node.X, node.Y] = new WhitePiece(FixPosition.X, FixPosition.Y);
            }
            lastPlaceNode = node;
            return PieceArray[node.X, node.Y];
        }
        //將棋盤座標回歸換算成滑鼠位子，以利生成棋子時帶入座標
        private Point pieceForm(Point nodeID) 
        {
            Point newpieceForm=new Point();
            newpieceForm.X = nodeID.X * DISTANCE + BOUNDARY;
            newpieceForm.Y = nodeID.Y * DISTANCE + BOUNDARY;
            return newpieceForm;
        }
        //將x,y帶入函式距離計算，換算靠近的節點或者都不在節點附近
        //換算後成為棋盤座標，以利能帶入array中變成紀錄
        private Point pieceClose(int x, int y) 
        {
            int nodeX = distanceClose(x);
            if (nodeX == -1|| nodeX>= NODE_COUNT)
            {
                return NO_MATCH_NODE;
            }
            int nodeY = distanceClose(y);
            if (nodeY == -1|| nodeX >= NODE_COUNT) 
            {
                return NO_MATCH_NODE;
            }
            return new Point(nodeX, nodeY);
        }
        //設定輸入值是否在旗子判定距離內，做出歸納計算旗盤座標
        private int distanceClose(int d) 
        {
            if (d < BOUNDARY- PIECERANGE) 
            {
                return -1;
            }
            d -= BOUNDARY;
            int quotient = d / DISTANCE;
            int remainder = d % DISTANCE;
            if (remainder <= PIECERANGE)
            {
                return quotient;
            }
            else if (remainder >= DISTANCE - PIECERANGE)
            {
                return quotient + 1;
            }
            else 
            {
                return -1;
            }
        }
        //給予棋盤座標，回傳上面棋子的顏色
        //透過PieceArray這個陣列(棋盤座標)，能找到儲存的棋子
        public ColorType GetPiceType(int x, int y) 
        {
            if (PieceArray[x, y] == null) 
            {
                return ColorType.Null;
            }
            return PieceArray[x, y].GetColor();
        }
        public void clearboard()
        {
            Array.Clear(PieceArray,0, PieceArray.GetLength(0) * PieceArray.GetLength(1)); 
            //GetLength(0) 是第一維的長度，而 GetLength(1) 是第二維的長度，相乘就是總的元素數量
        }
    }
}
